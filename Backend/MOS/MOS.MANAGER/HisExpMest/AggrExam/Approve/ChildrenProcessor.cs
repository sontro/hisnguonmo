using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Approve
{
    class ChildrenProcessor : BusinessBase
    {
        internal ChildrenProcessor()
            : base()
        {
        }

        internal ChildrenProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(long aggrExpMestId, string loginName, string userName, ref List<string> sqls)
        {
            try
            {
                string sqlExpMest = string.Format("UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = {0} WHERE AGGR_EXP_MEST_ID = {1}", IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE, aggrExpMestId);
                string sqlServiceReq = string.Format("UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = {0}, EXECUTE_LOGINNAME = '{1}', EXECUTE_USERNAME = '{2}' WHERE ID IN (SELECT SERVICE_REQ_ID FROM HIS_EXP_MEST WHERE AGGR_EXP_MEST_ID = {3})", IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL, loginName, userName, aggrExpMestId);

                sqls.Add(sqlExpMest);
                sqls.Add(sqlServiceReq);

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
