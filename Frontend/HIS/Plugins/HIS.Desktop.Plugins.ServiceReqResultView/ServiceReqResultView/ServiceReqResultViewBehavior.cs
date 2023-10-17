using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ServiceReqResultView;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace HIS.Desktop.Plugins.ServiceReqResultView.ServiceReqResultView
{
    public sealed class ServiceReqResultViewBehavior : Tool<IDesktopToolContext>, IServiceReqResultView
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long sereServid;
        public ServiceReqResultViewBehavior()
            : base()
        {
        }

        public ServiceReqResultViewBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IServiceReqResultView.Run()
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
                            sereServid = (long)item;
                        }
                        if (currentModule != null && sereServid > 0)
                        {
                            result = new frmServiceReqResultView(currentModule, sereServid);
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
