using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSchedule
{
    class HisRationScheduleExecute : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServRationCreate hisSereServRationCreate;
        private HisRationScheduleUpdate hisRationScheduleUpdate;

        internal HisRationScheduleExecute()
            :base()
        {
            this.Init();
        }

        internal HisRationScheduleExecute(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
            this.hisSereServRationCreate = new HisSereServRationCreate(param);
            this.hisRationScheduleUpdate = new HisRationScheduleUpdate(param);
        }

        internal bool Run(RationScheduleExecuteSDO data, ref List<V_HIS_SERVICE_REQ_10> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisRationScheduleCheck checker = new HisRationScheduleCheck(param);

                valid = valid && checker.VerifyField(data);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtRoom(data.ReqRoomId, workPlace.RoomId);
                if (valid)
                {
                    resultData = new List<V_HIS_SERVICE_REQ_10>();
                    var filterRationSchedule = new HisRationScheduleViewFilterQuery();
                    filterRationSchedule.LAST_DEPARTMENT_ID = workPlace.DepartmentId;
                    filterRationSchedule.FROM_DATE_TO = data.DateTo;
                    filterRationSchedule.TO_DATE_FROM = data.DateFrom;
                    filterRationSchedule.IS_PAUSE = false;
                    var listRationSchedule = new HisRationScheduleGet().GetView(filterRationSchedule);
                    if (IsNotNullOrEmpty(listRationSchedule))
                    {
                        var treatments = new HisTreatmentGet().GetByIds(listRationSchedule.Select(o=>o.TREATMENT_ID ?? 0).ToList());
                        //Tạo thông tin y lệnh suất ăn ứng với các thông tin báo ăn
                        List<HIS_SERVICE_REQ> serviceReqsCreate = null;
                        List<HIS_SERE_SERV_RATION> sereServRations = null;
                        ProcessRationSchedule(data, listRationSchedule, workPlace, treatments, ref serviceReqsCreate);
                        ProcessSereServRation(data, listRationSchedule, serviceReqsCreate, ref sereServRations);

                        
                        if (IsNotNullOrEmpty(serviceReqsCreate))
                        {
                            //Cập nhật ngày chỉ định cuối cùng (đã update trong hàm ProcessRationSchedule())
                            var rationScheduleIDs = serviceReqsCreate.Select(seq=>seq.RATION_SCHEDULE_ID ?? 0);
                            var rationScheduleChanges = listRationSchedule.Where(o => rationScheduleIDs.Contains(o.ID)).ToList();
                            Mapper.CreateMap<V_HIS_RATION_SCHEDULE, HIS_RATION_SCHEDULE>();
                            List<HIS_RATION_SCHEDULE> listUpdate= Mapper.Map<List<HIS_RATION_SCHEDULE>>(rationScheduleChanges);
                            if (!this.hisRationScheduleUpdate.UpdateList(listUpdate))
                            {
                                throw new Exception("hisRationScheduleUpdate, Ket thuc nghiep vu");
                            }

                            resultData = new HisServiceReqGet().GetView10ByIds(serviceReqsCreate.Select(seq => seq.ID).ToList());
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                resultData = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rolback();
                result = false;
            }
            return result;
        }

        private void ProcessSereServRation(RationScheduleExecuteSDO data, List<V_HIS_RATION_SCHEDULE> listRationSchedule, List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_SERE_SERV_RATION> sereServRations)
        {
            if (IsNotNullOrEmpty(serviceReqs))
            {
                sereServRations = this.MakeDataSereServRation(serviceReqs, listRationSchedule);
                if (!this.hisSereServRationCreate.CreateList(sereServRations))
                {
                    throw new Exception("Tao thong tin chi tiet xuat an that bai. Ket thuc nghiep vu");
                }
            }
        }

        private List<HIS_SERE_SERV_RATION> MakeDataSereServRation(List<HIS_SERVICE_REQ> serviceReqs, List<V_HIS_RATION_SCHEDULE> listRationSchedule)
        {
            List<HIS_SERE_SERV_RATION> result = new List<HIS_SERE_SERV_RATION>();
            foreach (var req in serviceReqs)
            {
                var rationSchedule = listRationSchedule.FirstOrDefault(o=>o.ID == req.RATION_SCHEDULE_ID);
                if (rationSchedule != null)
                {
                    HIS_SERE_SERV_RATION toInsert = new HIS_SERE_SERV_RATION();
                    toInsert.SERVICE_REQ_ID = req.ID;
                    toInsert.SERVICE_ID = rationSchedule.SERVICE_ID ?? 0;
                    if (req.IS_FOR_AUTO_CREATE_RATION == Constant.IS_TRUE)
                    {
                        toInsert.AMOUNT = (rationSchedule.AMOUNT ?? 0) / 2;
                    }
                    else
                    {
                        toInsert.AMOUNT = rationSchedule.AMOUNT ?? 0;
                    }

                    toInsert.PATIENT_TYPE_ID = rationSchedule.PATIENT_TYPE_ID ?? 0;
                    toInsert.INSTRUCTION_NOTE = rationSchedule.NOTE;
                    result.Add(toInsert);
                }
            }
            return result;
        }

        private void ProcessRationSchedule(RationScheduleExecuteSDO data, List<V_HIS_RATION_SCHEDULE> listRationSchedule, WorkPlaceSDO workPlace, List<HIS_TREATMENT> treatments, ref List<HIS_SERVICE_REQ> serviceReqsCreate)
        {
            if (serviceReqsCreate == null)
                serviceReqsCreate = new List<HIS_SERVICE_REQ>();
            DateTime from = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.DateFrom).Value;
            DateTime to = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.DateTo).Value;
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                var iDate = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(day.Date).Value;
                var rationSchedules = listRationSchedule.Where(o => o.VIR_FROM_DATE <= iDate
                                                        && (o.LAST_ASSIGN_DATE == null || o.LAST_ASSIGN_DATE < iDate)
                                                        && (o.VIR_TO_DATE == null || o.VIR_TO_DATE >= iDate)).ToList();
                if (!IsNotNullOrEmpty(rationSchedules)) continue;
                foreach (var item in rationSchedules)
                {
                    V_HIS_REFECTORY refectory = HisRefectoryCFG.DATA.Where(o => o.ID == item.REFECTORY_ROOM_ID).FirstOrDefault();
                    var treatment = treatments.FirstOrDefault(o => o.ID == item.TREATMENT_ID);
                    if (treatment == null) continue;
                    HIS_SERVICE_REQ dataAdd = new HIS_SERVICE_REQ();
                    dataAdd.SESSION_CODE = Guid.NewGuid().ToString();
                    dataAdd.EXECUTE_DEPARTMENT_ID = refectory != null ? refectory.DEPARTMENT_ID : 0;
                    dataAdd.EXECUTE_ROOM_ID = refectory.ROOM_ID;
                    dataAdd.RATION_TIME_ID = item.RATION_TIME_ID;
                    if (day.Date == DateTime.Today)
                    {
                        dataAdd.INTRUCTION_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    }
                    else
                    {
                        dataAdd.INTRUCTION_TIME = item.FROM_TIME ?? 0;
                    }
                    dataAdd.ICD_CODE = item.ICD_CODE;
                    dataAdd.ICD_NAME = item.ICD_NAME;
                    dataAdd.ICD_SUB_CODE = item.ICD_SUB_CODE;
                    dataAdd.ICD_TEXT = item.ICD_TEXT;
                    dataAdd.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    dataAdd.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN;
                    dataAdd.TREATMENT_ID = item.TREATMENT_ID ?? 0;
                    dataAdd.TDL_TREATMENT_CODE = treatment.TREATMENT_CODE;
                    dataAdd.REQUEST_ROOM_ID = data.ReqRoomId;
                    dataAdd.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                    dataAdd.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    dataAdd.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    dataAdd.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(dataAdd.REQUEST_LOGINNAME);
                    dataAdd.TREATMENT_TYPE_ID = item.TDL_TREATMENT_TYPE_ID;
                    dataAdd.IS_FOR_HOMIE = item.IS_FOR_HOMIE;
                    dataAdd.IS_FOR_AUTO_CREATE_RATION = IsForAutoCreateRation(item, iDate) ? (short?)Constant.IS_TRUE : null;
                    dataAdd.RATION_SCHEDULE_ID = item.ID;
                    HisServiceReqUtil.SetTdl(dataAdd, item);
                    if (dataAdd != null)
                    {
                        serviceReqsCreate.Add(dataAdd);
                        //Cập nhật ngày chỉ định cuối cùng
                        item.LAST_ASSIGN_DATE = iDate;
                    }
                }
            }
            if (IsNotNullOrEmpty(serviceReqsCreate) && !this.hisServiceReqCreate.CreateList(serviceReqsCreate))
            {
                throw new Exception("Tao thong tin y lenh xuat an that bai. Ket thuc nghiep vu");
            }
        }

        private bool IsForAutoCreateRation(V_HIS_RATION_SCHEDULE item, long date)
        {
            if (item.IS_HALF_IN_FIRST_DAY == Constant.IS_TRUE && item.VIR_FROM_DATE == date)
                return true;
            else
                return false;
        }

        private void Rolback()
        {
            try
            {
                this.hisRationScheduleUpdate.RollbackData();
                this.hisSereServRationCreate.RollbackData();
                this.hisServiceReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
