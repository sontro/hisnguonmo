using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.GetValue
{
    class GetValueBehavior : IGetValue
    {
        UserControl control;
        public GetValueBehavior()
            : base()
        { }

        public GetValueBehavior(CommonParam param, UserControl uc)
            : base()
        {
            this.control = uc;
        }

        object IGetValue.Run()
        {
            try
            {
                return ((UCCauseOfDeath)this.control).GetValue();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
