using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CheckCross;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.CheckCross.CheckCross
{
    public sealed class CheckCrossBehavior : Tool<IDesktopToolContext>, ICheckCross
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public CheckCrossBehavior()
            : base()
        {
        }

        public CheckCrossBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICheckCross.Run()
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
                    }
                    if (currentModule != null)
                    {
                          result = new frmCheckCross(currentModule);
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
