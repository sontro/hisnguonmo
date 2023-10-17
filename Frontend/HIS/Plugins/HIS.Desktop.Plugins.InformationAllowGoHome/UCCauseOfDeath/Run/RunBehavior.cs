using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.Run
{
    public sealed class RunBehavior : IRun
    {
        CauseOfDeathADO entity;
        public RunBehavior()
            : base()
        {
        }

        public RunBehavior(CommonParam param, CauseOfDeathADO ado)
            : base()
        {
            entity = ado;
        }

        object IRun.Run()
        {
            try
            {
                return new UCCauseOfDeath(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
