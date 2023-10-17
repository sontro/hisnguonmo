using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteList.LisDeliveryNoteList
{
    class LisDeliveryNoteListBehavior : Tool<IDesktopToolContext>, ILisDeliveryNoteList
    {
        object[] entity;
        string loginName;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        internal LisDeliveryNoteListBehavior()
            : base()
        {

        }

        internal LisDeliveryNoteListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ILisDeliveryNoteList.Run()
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
                    return new HIS.Desktop.Plugins.LisDeliveryNoteList.Run.UCLisDeliveryNoteList(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
