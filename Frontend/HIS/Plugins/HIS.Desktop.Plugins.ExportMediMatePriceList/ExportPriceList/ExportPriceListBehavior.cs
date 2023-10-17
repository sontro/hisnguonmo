using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ExportMediMatePriceList.ExportPriceList
{
    class ExportPriceListBehavior : BusinessBase, IExportPriceList
    {
        object[] entity;
        internal ExportPriceListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExportPriceList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                short MediMatiType = 0;
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is short)
                            {
                                MediMatiType = (short)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new frmExportPriceList(moduleData, MediMatiType);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }
    }
}
