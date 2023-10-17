using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.SetValue
{
    class SetValueBehavior : ISetValue
    {
        UserControl control;
        CauseOfDeathADO entity;
        internal SetValueBehavior()
            : base()
        { }

        internal SetValueBehavior(CommonParam param, UserControl uc, CauseOfDeathADO data)
            : base()
        {
            this.control = uc;
            this.entity = data;
        }

        void ISetValue.Run()
        {
            try
            {
                ((UCCauseOfDeath)this.control).SetValue(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
