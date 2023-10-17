using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Confirm.PresBloodConfirm
{
    class HisExpMestPresBloodConfirm : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private ExpMestBltyReqProcessor expMestBltyReqProcessor;

        internal HisExpMestPresBloodConfirm(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestBltyReqProcessor = new ExpMestBltyReqProcessor(param);
        }

        internal bool Run(HisExpMestConfirmSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_EXP_MEST raw = null;
                List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;
                HisExpMestConfirmCheck checker = new HisExpMestConfirmCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && checker.VerifyData(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && commonChecker.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && checker.WorkingInMediStock(raw, workPlace);
                valid = valid && checker.IsPresBlood(raw);
                valid = valid && checker.IsStatusRequest(raw);
                valid = valid && checker.IsNotConfirm(raw);
                valid = valid && checker.ValidData(data, ref expMestBltyReqs);
                if (valid)
                {
                    if (!this.expMestProcessor.Run(raw))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestBltyReqProcessor.Run(data, expMestBltyReqs))
                    {
                        throw new Exception("expMestBltyReqProcessor. Rollback du lieu");
                    }
                    result = true;
                    resultData = raw;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_ChotPhieuXuatDonMau)
                    .TreatmentCode(raw.TDL_TREATMENT_CODE)
                    .ServiceReqCode(raw.TDL_SERVICE_REQ_CODE)
                    .ExpMestCode(raw.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.expMestBltyReqProcessor.Rollback();
                this.expMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
