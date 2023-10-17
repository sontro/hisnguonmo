using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.RegisterNumber;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.RegisterNumber.RegisterNumber
{
    public sealed class RegisterNumberBehavior : Tool<IDesktopToolContext>, IRegisterNumber
    {
        object[] entity;
        public RegisterNumberBehavior()
            : base()
        {
        }

        public RegisterNumberBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IRegisterNumber.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            result = new frmRegisterNumber((Inventec.Desktop.Common.Modules.Module)item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
