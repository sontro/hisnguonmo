using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Export
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


        internal bool Run(HIS_EXP_MEST expMest, string loginname, string username, long finishTime, ref List<string> sqls)
        {
            try
            {
                if (IsNotNull(expMest) && expMest.SERVICE_REQ_ID.HasValue)
                {
                    string sqlServiceReq = String.Format("UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = {0}, FINISH_TIME = {1}, EXECUTE_LOGINNAME = '{2}', EXECUTE_USERNAME = '{3}' WHERE ID = {4}", IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT, finishTime, loginname, username, expMest.SERVICE_REQ_ID.Value);
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
