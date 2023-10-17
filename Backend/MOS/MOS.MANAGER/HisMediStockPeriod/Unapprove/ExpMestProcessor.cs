using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class ExpMestProcessor : BusinessBase
    {
        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    string sql = String.Format("DELETE HIS_EXP_MEST WHERE ID = {0}", expMest.ID);
                    sqls.Add(sql);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
