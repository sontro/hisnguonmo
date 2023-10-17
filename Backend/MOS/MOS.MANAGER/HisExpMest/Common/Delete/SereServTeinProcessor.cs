using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class SereServTeinProcessor : BusinessBase
    {
        internal SereServTeinProcessor(CommonParam param)
            : base(param)
        {
        
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    string update = string.Format("UPDATE HIS_SERE_SERV_TEIN SET EXP_MEST_ID = NULL WHERE EXP_MEST_ID = {0} ", expMest.ID);
                    listSql.Add(update);
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
