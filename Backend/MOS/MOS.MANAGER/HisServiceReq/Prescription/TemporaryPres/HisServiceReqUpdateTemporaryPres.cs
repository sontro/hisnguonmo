using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.TemporaryPres
{
    class HisServiceReqUpdateTemporaryPres: BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqUpdateTemporaryPres()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqUpdateTemporaryPres(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(TemporaryServiceReqSDO data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = null;

                HisServiceReqCheck commomChecker = new HisServiceReqCheck(param);
                HisServiceReqUpdateTemporaryPresCheck checker = new HisServiceReqUpdateTemporaryPresCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commomChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && checker.IsValidTemporaryPres(serviceReq);
                if (valid)
                {
                    serviceReq.IS_TEMPORARY_PRES = null;
                    serviceReq.INTRUCTION_TIME = data.InstructionTime;
                    serviceReq.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceReq.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (!this.hisServiceReqUpdate.Update(serviceReq, false))
                    {
                        throw new Exception("Cap nhat don tam that bai. Rollback du lieu");
                    }

                    result = true;
                    resultData = serviceReq;

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_SuDungDonTam)
                        .ServiceReqCode(resultData.SERVICE_REQ_CODE)
                        .Run();
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
        }
    }
}
