using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.ExroRoomImport;

namespace Inventec.Desktop.Plugins.ExroRoomImport
{
    public sealed class ExroRoomImportBehavior : Tool<IDesktopToolContext>, IExroRoomImport
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        RefeshReference delegateRefresh;
        public ExroRoomImportBehavior()
            : base()
        {
        }

        public ExroRoomImportBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExroRoomImport.Run()
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
                        else if (item is RefeshReference)
                        {
                            delegateRefresh = (RefeshReference)item;
                        }
                    }
                    if (currentModule != null && delegateRefresh != null)
                    {
                        result = new frmImportExroRoom(currentModule, delegateRefresh);
                    }
                    else
                    {
                        result = new frmImportExroRoom(currentModule);
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
