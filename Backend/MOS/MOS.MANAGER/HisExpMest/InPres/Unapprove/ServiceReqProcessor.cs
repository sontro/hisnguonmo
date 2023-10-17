using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unapprove
{
    class ServiceReqProcessor : BusinessBase
    {
        internal ServiceReqProcessor()
            : base()
        {

        }

        internal ServiceReqProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                if (expMest.SERVICE_REQ_ID.HasValue)
                {
                    string sqlServiceReq = string.Format("UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = {0}, EXECUTE_LOGINNAME = NULL, EXECUTE_USERNAME = NULL, START_TIME = NULL WHERE ID = {1}", IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL, expMest.SERVICE_REQ_ID.Value);

                    sqls.Add(sqlServiceReq);
                }

                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
