using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.DrugInterventionInfo
{
    class DrugInterventionInfoBehavior : BusinessBase, IDrugInterventionInfo
    {
         object[] entity;
         internal DrugInterventionInfoBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

         object IDrugInterventionInfo.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long  id = 0;
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
                            else if (entity[i] is long)
                            {
                                id = (long)entity[i];
                            }
                        }
                    }
                }

                return new frmDrugInterventionInfo(moduleData, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
