using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisServiceReq.Pacs.Start
{
    partial class HisServiceReqPacsStart : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisSereServExtCreate hisSereServExtCreate;

        internal HisServiceReqPacsStart()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqPacsStart(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        /// <summary>
        /// Bat dau xu ly dich vu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(string accessionNUmber)
        {
            bool result = false;
            try
            {
                HisServiceReqPacsStartCheck checker = new HisServiceReqPacsStartCheck(param);
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_SERE_SERV_EXT> sereServExts = null;
                HIS_SERE_SERV sereServ = null;
                HIS_TREATMENT hisTreatment = null;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                bool valid = checker.IsValidData(accessionNUmber, ref sereServ, ref serviceReq, ref sereServExts);
                valid = valid && treatmentChecker.IsUnLock(serviceReq.TREATMENT_ID, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);

                if (valid)
                {
                    long now = Inventec.Common.DateTime.Get.Now().Value;
                    this.ProcessSereServExt(sereServExts, sereServ, serviceReq, now);
                    this.ProcessServiceReq(serviceReq, now);
                        
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            this.hisSereServExtCreate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
        }

        private void ProcessSereServExt(List<HIS_SERE_SERV_EXT> sereServExts, HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq, long now)
        {
            HIS_SERE_SERV_EXT sereServExt = sereServExts != null ? sereServExts.Where(o => o.SERE_SERV_ID == sereServ.ID).FirstOrDefault() : null;

            if (sereServExt == null)
            {
                sereServExt = new HIS_SERE_SERV_EXT();
                sereServExt.SERE_SERV_ID = sereServ.ID;
                sereServExt.BEGIN_TIME = now;
                sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;

                HisSereServExtUtil.SetTdl(sereServExt, sereServ);

                if (!this.hisSereServExtCreate.Create(sereServExt))
                {
                    throw new Exception("Tao HIS_SERE_SERV_EXT that bai");
                }
            }
            else if (sereServExt != null && !sereServExt.BEGIN_TIME.HasValue)
            {
                sereServExt.BEGIN_TIME = now;
                sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;

                if (!this.hisSereServExtUpdate.Update(sereServExt, false))
                {
                    throw new Exception("Cap nhat HIS_SERE_SERV_EXT that bai");
                }
            }
        }

        private void ProcessServiceReq(HIS_SERVICE_REQ serviceReq, long now)
        {
            if (serviceReq != null && serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
            {
                serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                serviceReq.START_TIME = now;
                serviceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                serviceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                if (!this.hisServiceReqUpdate.Update(serviceReq, false))
                {
                    throw new Exception("Cap nhat HIS_SERVICE_REQ that bai");
                }
                new EventLogGenerator(EventLog.Enum.HisServiceReq_BatDauXuLy)
                            .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                            .ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();
            }
        }
    }
}
