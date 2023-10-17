using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.OtherFormAssService;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace HIS.Desktop.Plugins.OtherFormAssService.OtherFormAssService
{
    public sealed class OtherFormAssServiceBehavior : Tool<IDesktopToolContext>, IOtherFormAssService
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long _serviceReqId;
        public OtherFormAssServiceBehavior()
            : base()
        {
        }

        public OtherFormAssServiceBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IOtherFormAssService.Run()
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
                        else if (item is long)
                        {
                            _serviceReqId = (long)item;
                        }
                        if (currentModule != null && _serviceReqId > 0)
                        {
                            result = new frmOtherFormAssService(currentModule, _serviceReqId);
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
