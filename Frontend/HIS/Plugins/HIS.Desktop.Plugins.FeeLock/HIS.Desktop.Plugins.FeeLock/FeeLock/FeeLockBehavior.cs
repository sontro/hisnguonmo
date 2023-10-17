using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.FeeLock;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.FeeLock.FeeLock
{
    public sealed class FeeLockBehavior : Tool<IDesktopToolContext>, IFeeLock
    {
        object entity;
        public FeeLockBehavior()
            : base()
        {
        }

        public FeeLockBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IFeeLock.Run()
        {
            try
            {
                return new frmFeeLock();
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
