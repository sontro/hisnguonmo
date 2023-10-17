using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BedLog;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.BedLog.BedLog
{
    public sealed class BedLogBehavior : Tool<IDesktopToolContext>, IBedLog
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
		
        public BedLogBehavior()
            : base()
        {
        }

        public BedLogBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IBedLog.Run()
        {
            try
            {
				if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        
                    }
                }
                if (moduleData != null)
                {
                    return new frmCreateBedlog(moduleData);
                }
                else
                {
                    return null;
                }
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
