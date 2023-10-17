using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIS.EFMODEL.DataModels;
using HIS.Desktop.Common;

namespace HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate.LisDeliveryNoteCreateUpdate
{
    class LisDeliveryNoteCreateUpdateBehavior : Tool<IDesktopToolContext>, ILisDeliveryNoteCreateUpdate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        LIS_DELIVERY_NOTE lisDeliveryNote = null;
        RefeshReference refeshReference;
        internal LisDeliveryNoteCreateUpdateBehavior()
            : base()
        {

        }

        internal LisDeliveryNoteCreateUpdateBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }
        object ILisDeliveryNoteCreateUpdate.Run()
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
                            this.moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is LIS_DELIVERY_NOTE)
                        {
                            lisDeliveryNote = (LIS_DELIVERY_NOTE)item;
                        }
                        else if (item is RefeshReference)
                        {
                            refeshReference = (RefeshReference)item;
                        }
                    }
                    result = new UCLisDeliveryNoteCreateUpdate(moduleData, lisDeliveryNote, refeshReference);

                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return result;
            }
            return result;
        }
    }
}
