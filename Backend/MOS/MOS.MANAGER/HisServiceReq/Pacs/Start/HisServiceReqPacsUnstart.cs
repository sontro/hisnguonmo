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
    partial class HisServiceReqPacsUnstart : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServExtUpdate hisSereServExtUpdate;

        internal HisServiceReqPacsUnstart()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqPacsUnstart(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
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
                    this.ProcessSereServExt(sereServExts, sereServ, serviceReq);
                    this.ProcessServiceReq(serviceReq, sereServExts);
                        
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
            this.hisServiceReqUpdate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
        }

        private void ProcessSereServExt(List<HIS_SERE_SERV_EXT> sereServExts, HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq)
        {
            HIS_SERE_SERV_EXT sereServExt = sereServExts != null ? sereServExts.Where(o => o.SERE_SERV_ID == sereServ.ID).FirstOrDefault() : null;

            //Neu co thoi gian bat dau va chua co thong tin xu ly thi cap nhat bo thoi gian bat dau
            if (sereServExt != null && sereServExt.BEGIN_TIME.HasValue
                && string.IsNullOrWhiteSpace(sereServExt.CONCLUDE)
                && string.IsNullOrWhiteSpace(sereServExt.DESCRIPTION)
                && string.IsNullOrWhiteSpace(sereServExt.DESCRIPTION_SAR_PRINT_ID)
                && !sereServExt.FILM_SIZE_ID.HasValue
                && !sereServExt.IS_FEE.HasValue
                && !sereServExt.IS_GATHER_DATA.HasValue
                && string.IsNullOrWhiteSpace(sereServExt.JSON_FORM_ID)
                //&& string.IsNullOrWhiteSpace(sereServExt.JSON_PRINT_ID)
                && string.IsNullOrWhiteSpace(sereServExt.MACHINE_CODE)
                && !sereServExt.MACHINE_ID.HasValue
                && string.IsNullOrWhiteSpace(sereServExt.NOTE)
                && !sereServExt.NUMBER_OF_FAIL_FILM.HasValue
                && !sereServExt.NUMBER_OF_FILM.HasValue
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_NURSE_LOGINNAME)
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_NURSE_USERNAME)
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_PRES_LOGINNAME)
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_PRES_USERNAME)
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_RESULT_LOGINNAME)
                && string.IsNullOrWhiteSpace(sereServExt.SUBCLINICAL_RESULT_USERNAME)
                )
            {
                sereServExt.BEGIN_TIME = null;
                sereServExt.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                sereServExt.TDL_TREATMENT_ID = sereServ.TDL_TREATMENT_ID;

                if (!this.hisSereServExtUpdate.Update(sereServExt, false))
                {
                    throw new Exception("Cap nhat HIS_SERE_SERV_EXT that bai");
                }
            }
        }

        private void ProcessServiceReq(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV_EXT> sereServExts)
        {
            //Neu y lenh dang o trang thai "dang xu ly" va ko ton tai HIS_SERE_SERV_EXT co thoi gian bat dau
            //thi cap nhat lai thong tin cua service_req
            if (serviceReq != null 
                && serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL
                && (!IsNotNullOrEmpty(sereServExts) || !sereServExts.Exists(t => t.BEGIN_TIME.HasValue)))
            {
                serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                serviceReq.START_TIME = null;
                serviceReq.EXECUTE_LOGINNAME = null;
                serviceReq.EXECUTE_USERNAME = null;

                if (!this.hisServiceReqUpdate.Update(serviceReq, false))
                {
                    throw new Exception("Cap nhat HIS_SERVICE_REQ that bai");
                }
                new EventLogGenerator(EventLog.Enum.HisServiceReq_HuyBatDau)
                            .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                            .ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();
            }
        }
    }
}
