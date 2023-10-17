using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BloodTypeInStock;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.BloodTypeInStock.BloodTypeInStock
{
    public sealed class BloodTypeInStockBehavior : Tool<IDesktopToolContext>, IBloodTypeInStock
    {
        object entity;
        public BloodTypeInStockBehavior()
            : base()
        {
        }

        public BloodTypeInStockBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IBloodTypeInStock.Run()
        {
            try
            {
                return new UCBloodTypeInStock();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
