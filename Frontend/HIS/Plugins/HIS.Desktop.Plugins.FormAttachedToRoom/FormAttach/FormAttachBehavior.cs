using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.FormAttachedToRoom
{
    public sealed class FormAttachBehavior : Tool<IDesktopToolContext>, IFormAttach
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        //FormAttachInputADO FormAttachInputADO = null;
        public FormAttachBehavior()
            : base()
        {
        }

        public FormAttachBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IFormAttach.Run()
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

                    result = new frmFormAttachedToRoom(currentModule);
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
