using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Packing.Delete
{
    class PackingProcessor : BusinessBase
    {
        internal PackingProcessor()
            : base()
        {

        }

        internal PackingProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_DISPENSE dispense, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                sqls.Add(String.Format("DELETE HIS_DISPENSE WHERE ID = {0}", dispense.ID));
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
