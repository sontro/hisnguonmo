using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(OutPatientPresSDO data, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, List<long> mediStockIds, string sessionCode, HIS_SERVICE_REQ parentServiceReq, ref List<HIS_SERVICE_REQ> resultData, ref HIS_SERVICE_REQ outStockServiceReq)
        {
            try
            {
                List<HIS_SERVICE_REQ> serviceReqs = null;
                HIS_SERVICE_REQ osServiceReq = null;
                HIS_PATIENT_TYPE_ALTER usingPta = ptas != null ? ptas.Where(o => o.LOG_TIME <= data.InstructionTime).OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;
                HIS_PATIENT_TYPE_ALTER currentPta = ptas != null ? ptas.OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;

                this.MakeData(data, usingPta, currentPta, mediStockIds, sessionCode, parentServiceReq, ref serviceReqs, ref osServiceReq);

                if (IsNotNullOrEmpty(serviceReqs))
                {
                    if (!this.hisServiceReqCreate.CreateList(serviceReqs, treatment))
                    {
                        return false;
                    }
                    resultData = serviceReqs;
                    outStockServiceReq = osServiceReq;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        //Tao service_req dua vao thong tin chi dinh thuoc trong kho
        private void MakeData(OutPatientPresSDO data, HIS_PATIENT_TYPE_ALTER usingPta, HIS_PATIENT_TYPE_ALTER currentPta, List<long> mediStockIds, string sessionCode, HIS_SERVICE_REQ parentServiceReq, ref List<HIS_SERVICE_REQ> resultData, ref HIS_SERVICE_REQ outStockServiceReq)
        {
            if (mediStockIds == null)
            {
                mediStockIds = new List<long>();
            }

            //Neu co ke don thuoc ngoai kho va co cau hinh tach don thuoc ngoai kho
            //Hoac don chi co thuoc ngoai kho chu ko co thuoc trong kho
            //==> phai tao service_req rieng cho thuoc ngoai kho

            if (IsNotNullOrEmpty(data.ServiceReqMaties) || IsNotNullOrEmpty(data.ServiceReqMeties))
            {
                if (HisServiceReqCFG.PRESCRIPTION_SPLIT_OUT_MEDISTOCK ||
                    (!IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Materials)))
                {
                    if (mediStockIds == null)
                    {
                        mediStockIds = new List<long>();
                    }
                    mediStockIds.Add(0);//0 de danh dau "medi_stock_id" cua don ngoai kho
                }
            }

            if (IsNotNullOrEmpty(mediStockIds))
            {
                if (resultData == null)
                {
                    resultData = new List<HIS_SERVICE_REQ>();
                }
                foreach (long mediStockId in mediStockIds)
                {
                    V_HIS_MEDI_STOCK mediStock = null;
                    HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();
                    if (mediStockId == 0) //neu la don ngoai kho
                    {
                        //Neu co chon nha thuoc thi lay theo nha thuoc duoc chon
                        if (data.DrugStoreId.HasValue)
                        {
                            V_HIS_MEDI_STOCK room = HisMediStockCFG.DATA.Where(o => o.ID == data.DrugStoreId.Value).FirstOrDefault();
                            serviceReq.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
                            serviceReq.EXECUTE_ROOM_ID = room.ROOM_ID;
                        }
                        //Neu ko tu dong tao phieu xuat ban (ko xac dinh nha thuoc) thi lay thong tin phong/khoa xu ly theo phong/khoa chi dinh
                        else
                        {
                            V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault();
                            serviceReq.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
                            serviceReq.EXECUTE_ROOM_ID = data.RequestRoomId;
                        }
                        
                        serviceReq.SERVICE_REQ_TYPE_ID = data.IsCabinet ? IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT : IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;

                        if (mediStockId == 0)
                        {
                            outStockServiceReq = serviceReq;
                        }
                    }
                    else //neu la don ke trong kho thi phong xu ly lay theo kho
                    {
                        mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId).FirstOrDefault();
                        serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
                        serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;

                        if (mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT;
                        }
                        else
                        {
                            if (currentPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_BenhNhanDaVaoDieuTri);
                                throw new Exception("Benh nhan da va dieu tri khong cho phep tao don phong kham");
                            }
                            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;
                        }
                    }
                    
                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    serviceReq.PARENT_ID = data.ParentServiceReqId;
                    serviceReq.INTRUCTION_TIME = data.InstructionTime;
                    serviceReq.TREATMENT_ID = data.TreatmentId;
                    serviceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                    serviceReq.ICD_NAME = data.IcdName;
                    serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                    serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                    serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                    serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
                    serviceReq.REQUEST_ROOM_ID = data.RequestRoomId;
                    serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                    serviceReq.REQUEST_USERNAME = data.RequestUserName;
                    serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                    serviceReq.ADVISE = data.Advise;
                    serviceReq.USE_TIME = data.UseTime;
                    serviceReq.REMEDY_COUNT = data.RemedyCount;
                    serviceReq.TRACKING_ID = data.TrackingId;
                    serviceReq.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
                    serviceReq.PRESCRIPTION_TYPE_ID = (short)data.PrescriptionTypeId;
                    serviceReq.SESSION_CODE = sessionCode;
                    serviceReq.IS_KIDNEY = data.IsKidney ? (short?)Constant.IS_TRUE : null;
                    serviceReq.KIDNEY_TIMES = data.KidneyTimes;
                    serviceReq.IS_EXECUTE_KIDNEY_PRES = data.IsExecuteKidneyPres ? (short?)Constant.IS_TRUE : null;
                    serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
                    serviceReq.PRES_GROUP = data.PresGroup;
                    serviceReq.SPECIAL_MEDICINE_TYPE = data.SpecialMedicineType;
                    serviceReq.INTERACTION_REASON = data.InteractionReason;
                    serviceReq.PRIORITY = parentServiceReq != null ? parentServiceReq.PRIORITY : null;
                    serviceReq.PRIORITY_TYPE_ID = parentServiceReq != null ? parentServiceReq.PRIORITY_TYPE_ID : null;
                    //Neu co ke don phu (chon thuoc/vat tu ma ko co bean) thi can danh dau "la don phu"
                    serviceReq.IS_SUB_PRES = (IsNotNullOrEmpty(data.Materials) && data.Materials.Exists(o => !IsNotNullOrEmpty(o.MaterialBeanIds)))
                        || (IsNotNullOrEmpty(data.Medicines) && data.Medicines.Exists(o => !IsNotNullOrEmpty(o.MedicineBeanIds)))
                        ? (short?) Constant.IS_TRUE : null;

                    //Thoi gian su dung cua don lay theo thoi gian su dung lon nhat cua thuoc co trong don 
                    long? maxNumOfDays = IsNotNullOrEmpty(data.Medicines) ?
                        data.Medicines.Where(o => o.MediStockId == mediStockId && o.NumOfDays.HasValue).Max(o => o.NumOfDays) : null;
                    if (!maxNumOfDays.HasValue) maxNumOfDays = data.NumOfDays;
                    if (maxNumOfDays.HasValue)
                    {
                        long dayCount = maxNumOfDays.Value == 0 ? 1 : maxNumOfDays.Value;

                        DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.UseTime > 0 ? data.UseTime.Value : data.InstructionTime).Value;
                        DateTime useTimeTo = time.AddDays(dayCount - 1);
                        serviceReq.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                    }

                    resultData.Add(serviceReq);
                }
            }
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}