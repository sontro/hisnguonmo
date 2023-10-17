using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Confirm.PresBloodUnconfirm
{
    class HisExpMestPresBloodUnconfirm : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private ExpMestBltyReqProcessor expMestBltyReqProcessor;

        internal HisExpMestPresBloodUnconfirm(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.expMestBltyReqProcessor = new ExpMestBltyReqProcessor(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
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
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && commonChecker.HasWorkPlaceInfo(data.ReqRoomId, ref workPlace);
                valid = valid && checker.WorkingInMediStock(raw, workPlace);
                valid = valid && checker.IsPresBlood(raw);
                valid = valid && checker.IsStatusRequest(raw);
                valid = valid && checker.IsConfirm(raw);
                if (valid)
                {
                    expMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(data.ExpMestId);

                    if (!this.expMestProcessor.Run(raw))
                    {
                        throw new Exception("expMestProcessor. Rollback du lieu");
                    }

                    if (!this.expMestBltyReqProcessor.Run(expMestBltyReqs))
                    {
                        throw new Exception("expMestBltyReqProcessor. Rollback du lieu");
                    }
                    result = true;
                    resultData = raw;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyChotPhieuXuatDonMau)
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
