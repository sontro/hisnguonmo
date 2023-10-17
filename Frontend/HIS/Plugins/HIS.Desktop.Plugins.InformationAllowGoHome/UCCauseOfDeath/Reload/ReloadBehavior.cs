using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.Reload
{
    public sealed class ReloadBehavior : IReload
    {
        UserControl control;
        public ReloadBehavior()
            : base()
        {
        }

        public ReloadBehavior(CommonParam param, UserControl data)
            : base()
        {
            this.control = data;
        }

        void IReload.Run()
        {
            try
            {
                ((UCCauseOfDeath)this.control).Reload();                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
