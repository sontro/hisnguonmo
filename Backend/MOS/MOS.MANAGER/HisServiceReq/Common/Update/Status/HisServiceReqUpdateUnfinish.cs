using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq.Common.Update.Status;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MOS.MANAGER.HisServiceReq.Update.Finish
{
    partial class HisServiceReqUpdateUnfinish : BusinessBase
    {
        private HIS_SERVICE_REQ before = null;

        internal HisServiceReqUpdateUnfinish()
            : base()
        {

        }

        internal HisServiceReqUpdateUnfinish(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(long id)
        {
            HIS_SERVICE_REQ resultData = new HIS_SERVICE_REQ();
            return this.Run(id, ref resultData);
        }

        internal bool Run(long id, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                HIS_SERVICE_REQ raw = new HisServiceReqGet().GetById(id);

                bool verifyTreatment = HisServiceReqStatusCheck.IsNeedToVerifyTreatment(raw);
                return this.Unfinish(raw, verifyTreatment, ref resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Huy ket thuc
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Unfinish(HIS_SERVICE_REQ raw, bool verifyTreatment, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);

                HIS_TREATMENT hisTreatment = null;

                valid = valid && this.IsFinish(raw);
                valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref hisTreatment);
                valid = valid && this.IsAllowUnfinishInHospitalization(raw, hisTreatment);
                if (verifyTreatment)
                {
                    valid = valid && treatmentChecker.IsUnLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                }

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    before = Mapper.Map<HIS_SERVICE_REQ>(raw);

                    //Chuyen trang thai sang trang thai "dang thuc hien"
                    raw.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                    raw.FINISH_TIME = null;
                    //luu du thua du lieu
                    raw.TDL_PATIENT_ID = hisTreatment.PATIENT_ID;

                    if (!DAOWorker.HisServiceReqDAO.Update(raw))
                    {
                        LogSystem.Warn("Cap nhat thong tin his_service_req de huy ket thuc that bai");
                        return false;
                    }

                    resultData = raw;
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_HuyKetKetThucYLenh)
                        .TreatmentCode(raw.TDL_TREATMENT_CODE)
                        .ServiceReqCode(raw.SERVICE_REQ_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Kiem tra co cho phep huy ket thuc trong truong hop nhap vien hay khong
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        private bool IsAllowUnfinishInHospitalization(HIS_SERVICE_REQ serviceReq, HIS_TREATMENT treatment)
        {
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (HisServiceReqCFG.ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION
                    && treatment.CLINICAL_IN_TIME.HasValue
                    && serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH
                    && serviceReq.INTRUCTION_TIME < treatment.CLINICAL_IN_TIME.Value
                    && !HisEmployeeUtil.IsAdmin(loginName))
                {
                    string clinicalInTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.CLINICAL_IN_TIME.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhDuocChiDinhTruocKhiNhapVien, clinicalInTime);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool IsFinish(HIS_SERVICE_REQ serviceReq)
        {
            if (serviceReq != null &&
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT != serviceReq.SERVICE_REQ_STT_ID)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepHuyHoanThanhKhiDaHoanThanh);
                return false;
            }
            return true;
        }

        public void RollbackData()
        {
            if (this.before != null)
            {
                if (!DAOWorker.HisServiceReqDAO.Update(this.before))
                {
                    LogSystem.Warn("Huy ket thuc that bai. Rollback that bai");
                }
            }
        }
    }
}
