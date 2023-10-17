using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteDetail.LisDeliveryNoteDetail
{
    class LisDeliveryNoteDetailBehavior : Tool<IDesktopToolContext>, ILisDeliveryNoteDetail
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        V_LIS_DELIVERY_NOTE deliveryNote = null;
        RefeshReference refeshReference;
        internal LisDeliveryNoteDetailBehavior()
            : base()
        {

        }

        internal LisDeliveryNoteDetailBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }
        object ILisDeliveryNoteDetail.Run()
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
                        else if (item is V_LIS_DELIVERY_NOTE)
                        {
                            deliveryNote = (V_LIS_DELIVERY_NOTE)item;
                        }
                        else if (item is RefeshReference)
                        {
                            refeshReference = (RefeshReference)item;
                        }
                    }
                    if (moduleData != null && deliveryNote != null)
                    {
                        result = new UCLisDeliveryNoteDetail(moduleData, deliveryNote, refeshReference);
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("__moduleData ", moduleData));
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("__deliveryNote ", deliveryNote));
                        return null;
                    }
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
