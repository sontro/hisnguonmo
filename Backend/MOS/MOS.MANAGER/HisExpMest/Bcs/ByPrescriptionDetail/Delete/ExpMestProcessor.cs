using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Delete
{
    class ExpMestProcessor : BusinessBase
    {
        internal ExpMestProcessor()
            : base()
        {

        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_EXP_MEST raw, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                sqls.Add(String.Format("DELETE HIS_EXP_MEST WHERE ID = {0} ", raw.ID));
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
