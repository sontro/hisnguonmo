using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.MestPatientType.PatientTypeMediStock
{
    class PatientTypeMediStockBehavior : Tool<IDesktopToolContext>, IPatientTypeMediStock
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        V_HIS_MEDI_STOCK executeMediStock;

        internal PatientTypeMediStockBehavior()
            : base()
        {

        }

        internal PatientTypeMediStockBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IPatientTypeMediStock.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is V_HIS_MEDI_STOCK)
                        {
                            executeMediStock = (V_HIS_MEDI_STOCK)item;
                        }
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }               
                    }
                    if (executeMediStock != null)
                    {
                        result = new UCPatientTypeMediStock(executeMediStock, currentModule);
                    }
                    else if (currentModule!=null)
                    {
                        result = new UCPatientTypeMediStock(currentModule);
                    }
                    else
                    {
                        result = new UCPatientTypeMediStock();
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
