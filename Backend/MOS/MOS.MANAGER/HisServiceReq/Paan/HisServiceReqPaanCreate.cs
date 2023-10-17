using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.UTILITY;

namespace MOS.MANAGER.HisServiceReq.Paan
{
    /// <summary>
    /// Xu ly nghiep vu chi dinh sinh thiet (giai phau benh ly)
    /// </summary>
    partial class HisServiceReqPaanCreate : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisServiceReqPaanCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqPaanCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Create(HisPaanServiceReqSDO data, ref HisServiceReqResultSDO resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                HIS_PATIENT_TYPE_ALTER usingPta = null;
                string sessionCode = Guid.NewGuid().ToString();
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(data.TreatmentId);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                WorkPlaceSDO workPlace = null;

                bool valid = true;
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && HisServiceRoomUtil.IsProcessable(data.RoomId, data.ServiceId, param);
                valid = valid && checker.IsValidPatientTypeAlter(data.InstructionTime, ptas, ref usingPta);
                if (valid)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    HIS_SERE_SERV sereServ = null;
                    this.ProcessServiceReq(data, treatment, usingPta, sessionCode, ref serviceReq);
                    this.ProcessSereServ(treatment, serviceReq, data, ref sereServ);
                    this.PassResult(serviceReq, sereServ, ref resultData);

                    HisServiceReqLog.Run(resultData.ServiceReq, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_ChiDinhDichVu);
                    result = true;

                    if (sereServ.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && sereServ.VIR_TOTAL_PATIENT_PRICE > 0)
                    {
                        List<HIS_SERVICE_REQ> serviceReqs = new List<HIS_SERVICE_REQ>();
                        serviceReqs.Add(serviceReq);
                        if (this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace))
                        {
                            if (!new HisTransReqCreateByService(param).Run(treatment, serviceReqs, workPlace))
                            {
                                Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, ref HisServiceReqResultSDO resultData)
        {
            resultData = new HisServiceReqResultSDO();
            resultData.ServiceReq = new HisServiceReqGet().GetViewById(serviceReq.ID);
            resultData.SereServs = new HisSereServGet().GetViewByServiceReqId(serviceReq.ID);
        }

        private void ProcessSereServ(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HisPaanServiceReqSDO data, ref HIS_SERE_SERV sereServ)
        {
            HIS_SERE_SERV toInsert = new HIS_SERE_SERV();
            toInsert.SERVICE_REQ_ID = serviceReq.ID;
            toInsert.SERVICE_ID = data.ServiceId;
            toInsert.AMOUNT = data.Amount;
            toInsert.EKIP_ID = data.EkipId;
            toInsert.PATIENT_TYPE_ID = data.PatientTypeId;
            toInsert.PARENT_ID = data.SereServParentId;
            toInsert.OTHER_PAY_SOURCE_ID = treatment.OTHER_PAY_SOURCE_ID;

            if (data.IsOutParentFee.HasValue && data.IsOutParentFee.Value)
            {
                toInsert.IS_OUT_PARENT_FEE = MOS.UTILITY.Constant.IS_TRUE;
            }
            if (data.IsExpend.HasValue && data.IsExpend.Value)
            {
                toInsert.IS_EXPEND = MOS.UTILITY.Constant.IS_TRUE;
            }

            long executeBranchId = HisDepartmentCFG.DATA
                .Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID)
                .FirstOrDefault().BRANCH_ID;

            //Cap nhat thong tin gia theo dich vu moi
            HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
            if (!priceAdder.AddPrice(toInsert, serviceReq.INTRUCTION_TIME, executeBranchId, serviceReq.REQUEST_ROOM_ID, serviceReq.REQUEST_DEPARTMENT_ID, serviceReq.EXECUTE_ROOM_ID))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            if (!this.hisSereServCreate.Create(toInsert, serviceReq, false))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

            //Cap nhat ti le BHYT cho sere_serv
            if (!this.hisSereServUpdateHein.UpdateDb())
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
            sereServ = toInsert;
        }

        /// <summary>
        /// Xu ly du lieu service_req
        /// </summary>
        /// <param name="currentSereServ"></param>
        /// <param name="data"></param>
        /// <param name="currentServiceReq"></param>
        /// <param name="additionServiceReq"></param>
        private void ProcessServiceReq(HisPaanServiceReqSDO data, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER usingPta, string sessionCode, ref HIS_SERVICE_REQ serviceReq)
        {
            HIS_SERVICE_REQ toInsert = new HIS_SERVICE_REQ();
            toInsert.EXECUTE_DEPARTMENT_ID = HisRoomCFG.DATA.Where(o => o.ID == data.RoomId).FirstOrDefault().DEPARTMENT_ID;
            toInsert.EXECUTE_ROOM_ID = data.RoomId;
            //gan chan doan cua phieu chi dinh dv cu vao phieu chi dinh dv moi
            toInsert.ICD_NAME = data.IcdName;
            toInsert.ICD_SUB_CODE = data.IcdSubCode;
            toInsert.ICD_CODE = data.IcdCode;
            toInsert.ICD_CAUSE_CODE = data.IcdCauseCode;
            toInsert.ICD_CAUSE_NAME = data.IcdCauseName;
            toInsert.ICD_TEXT = data.IcdText;
            toInsert.INTRUCTION_TIME = data.InstructionTime;
            toInsert.PARENT_ID = data.ParentServiceReqId;
            toInsert.PRIORITY = data.Priority;
            toInsert.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            toInsert.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL;
            toInsert.TDL_PATIENT_ID = treatment.PATIENT_ID;
            toInsert.TREATMENT_ID = treatment.ID;
            toInsert.REQUEST_ROOM_ID = data.RequestRoomId;
            toInsert.REQUEST_LOGINNAME = data.RequestLoginName;
            toInsert.REQUEST_USERNAME = data.RequestUserName;
            toInsert.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
            toInsert.IS_EMERGENCY = data.IsMergency;
            toInsert.LIQUID_TIME = data.LiquidTime;
            toInsert.PAAN_LIQUID_ID = data.PaanLiquidId;
            toInsert.PAAN_POSITION_ID = data.PaanPositionId;
            toInsert.DESCRIPTION = data.Description;
            toInsert.TRACKING_ID = data.TrackingId;
            toInsert.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
            toInsert.SESSION_CODE = sessionCode;
            toInsert.BARCODE_LENGTH = HisServiceReqCFG.LIS_SID_LENGTH;//su dung truong barcode cho SID ben LIS (labconn, roche, ...)
            toInsert.TDL_SERVICE_IDS = data.ServiceId.ToString();
            toInsert.TEST_SAMPLE_TYPE_ID = data.TestSampleTypeId;

            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.ServiceId).FirstOrDefault();
            if (service != null)
            {
                toInsert.EXE_SERVICE_MODULE_ID = service.EXE_SERVICE_MODULE_ID.HasValue ? service.EXE_SERVICE_MODULE_ID : service.SETY_EXE_SERVICE_MODULE_ID;

                //xử lý khi tạo y lệnh nếu dịch vụ truyền lên có IS_NOT_REQUIRED_COMPLETE(HIS_SERVICE) = 1 thì Lưu thông tin IS_NOT_REQUIRED_COMPLETE = 1(HIS_SERVICE_REQ)
                toInsert.IS_NOT_REQUIRED_COMPLETE = service.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE ? (short?)Constant.IS_TRUE : null;
            }
            
            V_HIS_ROOM room = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault() : null;

            if (room != null)
            {
                toInsert.REQUEST_DEPARTMENT_ID = room.DEPARTMENT_ID;
            }

            if (!this.hisServiceReqCreate.Create(toInsert, treatment))
            {
                throw new Exception("Ket thuc nghiep vu");
            }

            serviceReq = toInsert;
        }

        //Rollback du lieu
        internal void RollbackData()
        {
            this.hisSereServCreate.RollbackData();

            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            //luu y: rollback service-req thi da xu bao gom ca du lieu paan_service_req
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
