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
    class ImpMestProcessor : BusinessBase
    {
        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (impMest != null)
                {
                    string sql = String.Format("DELETE HIS_IMP_MEST WHERE ID = {0}", impMest.ID);
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
