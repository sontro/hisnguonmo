using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Repay.Repay;

namespace HIS.Desktop.Plugins.Repay.Repay
{
    class RepayBehavior : BusinessBase, IRepay
    {
        object entity;
        internal RepayBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IRepay.Run()
        {
            try
            {
                if (this.entity is MOS.EFMODEL.DataModels.V_HIS_TREATMENT)
                {
                    return new frmRepay((MOS.EFMODEL.DataModels.V_HIS_TREATMENT)this.entity, null, 1,null);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
