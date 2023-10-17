using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Approve
{
    class MaterialProcessor : BusinessBase
    {
        internal MaterialProcessor()
            : base()
        {

        }

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, string loginname, string username, long? approvalTime, ref List<string> sqls)
        {
            try
            {
                string sql = string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET APPROVAL_TIME = {0}, APPROVAL_LOGINNAME = '{1}', APPROVAL_USERNAME ='{2}' WHERE EXP_MEST_ID = {3} ", approvalTime, loginname, username, expMest.ID);
                sqls.Add(sql);
                return true;
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
