using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO;
using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.Run;
using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.SetValue;
using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.Reload;
using HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.GetValue;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath
{
    public class UCCauseOfDeathProcessor : BussinessBase
    {
        object uc;
        public UCCauseOfDeathProcessor()
            : base()
        {
        }

        public UCCauseOfDeathProcessor(CommonParam paramBusiness)
            : base(paramBusiness)
        {
        }

        public void Reload(UserControl control)
        {
            try
            {
                IReload behavior = ReloadFactory.MakeIReload(param, (control == null ? (UserControl)uc : control));
                if (behavior != null) behavior.Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public object Run(CauseOfDeathADO data)
        {
            uc = null;
            try
            {
                IRun behavior = RunFactory.MakeIRun(param, data);
                uc = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return uc;
        }

        public void SetValue(UserControl control, CauseOfDeathADO data)
        {
            try
            {
                ISetValue behavior = SetValueFactory.MakeISetValue(param, (control == null ? (UserControl)uc : control), data);
                if (behavior != null) behavior.Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public object GetValue(UserControl control)
        {
            object result = null;
            try
            {
                IGetValue behavior = GetValueFactory.MakeIGetValue(param, (control == null ? (UserControl)uc : control));
                result = (behavior != null) ? behavior.Run() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }


    }
}
