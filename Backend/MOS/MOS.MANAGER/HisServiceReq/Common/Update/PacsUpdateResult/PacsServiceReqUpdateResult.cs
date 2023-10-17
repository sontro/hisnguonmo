using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisSereServ;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.PacsUpdateResult
{
    class PacsServiceReqUpdateResult : BusinessBase
    {
        private HisServiceReqUpdateUnfinish finishServiceReq;
        private HisSereServUpdate updateSereServ;

        internal PacsServiceReqUpdateResult()
            : base()
        {
            this.Init();
        }

        internal PacsServiceReqUpdateResult(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.finishServiceReq = new HisServiceReqUpdateUnfinish(param);
            this.updateSereServ = new HisSereServUpdate(param);
        }

        internal bool Run(HisPacsResultTDO data)
        {
            bool result = false;

            try
            {
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                HIS_SERE_SERV sereServ = null;

                PacsServiceReqUpdateResultCheck checker = new PacsServiceReqUpdateResultCheck(param);
                HisSereServCheck commonChecker = new HisSereServCheck(param);

                bool valid = true;
                valid = valid && checker.IsValidAccessionNumber(data.AccessionNumber, ref sereServ, ref serviceReq, ref treatment);
                valid = valid && commonChecker.HasExecute(sereServ);
                valid = valid && checker.IsValidCanCel(data.IsCancel, data.EndTime, treatment);
                if (valid)
                {
                    if (data.IsCancel)
                    {
                        if (serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
                        {
                            HIS_SERVICE_REQ resultData = null;
                            if (!finishServiceReq.Run(serviceReq.ID, ref resultData))
                            {
                                throw new Exception("");
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }

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
            this.finishServiceReq.RollbackData();
        }
    }
}
