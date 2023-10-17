using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ServicePatyListImport.ServicePatyListImport
{
    class ServicePatyListImportBehavior : HIS.Desktop.Common.BusinessBase, IServicePatyListImport
    {
        object[] entity;
        internal ServicePatyListImportBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IServicePatyListImport.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            List<V_HIS_SERVICE_PATY> hisServicePaty = new List<V_HIS_SERVICE_PATY>();
            try
            {

                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is List<V_HIS_SERVICE_PATY>)
                        {
                            hisServicePaty = (List<V_HIS_SERVICE_PATY>)item;
                        }
                    }
                }
                if (moduleData != null && hisServicePaty != null)
                {
                    return new frmServicePatyListImport(hisServicePaty, moduleData);
                }
                else
                {
                    return null;
                }
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
