using Inventec.Core;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unapprove
{
    class MedicineProcessor : BusinessBase
    {
        internal MedicineProcessor()
            : base()
        {

        }

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(long expMestId, ref List<string> sqls)
        {
            try
            {
                string sql = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET APPROVAL_TIME = NULL, APPROVAL_LOGINNAME = NULL, APPROVAL_USERNAME = NULL WHERE EXP_MEST_ID = {0} ", expMestId);
                sqls.Add(sql);
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
