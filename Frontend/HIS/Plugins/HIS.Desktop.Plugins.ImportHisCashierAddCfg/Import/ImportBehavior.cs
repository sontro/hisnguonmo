using HIS.Desktop.Common;
using HIS.Desktop.Plugins.ImportHisCashierAddCfg.FormLoad;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportHisCashierAddCfg.Import
{
    class ImportBehavior : BusinessBase, IImport
    { 
        object[] entity;

        internal ImportBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImport.Run()
        {
            object frm = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                RefeshReference refeshReference = null;


                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is RefeshReference)
                            {
                                refeshReference = (RefeshReference)entity[i];
                            }
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                if (moduleData != null && refeshReference != null)
                {
                    frm = new frmCashierAddCfg(moduleData, refeshReference);
                }
                else if (moduleData != null && refeshReference == null)
                {
                    frm = new frmCashierAddCfg(moduleData);
                }
                return frm;
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
