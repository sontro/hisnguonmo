using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisMachineServMaty;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisMachineServMaty.HisMachineServMaty
{
    public sealed class HisMachineServMatyBehavior : Tool<IDesktopToolContext>, IHisMachineServMaty
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public HisMachineServMatyBehavior()
            : base()
        {
        }

        public HisMachineServMatyBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMachineServMaty.Run()
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
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        
                        if (currentModule != null)
                        {
                            result = new UCHisMachineServMaty(currentModule);
                            break;
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
