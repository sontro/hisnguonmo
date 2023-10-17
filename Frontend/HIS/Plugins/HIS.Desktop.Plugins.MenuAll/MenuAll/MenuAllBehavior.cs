using DevExpress.Utils;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.ModuleExt;

namespace HIS.Desktop.Plugins.MenuAll.MenuAll
{
    class MenuAllBehavitor : BusinessBase, IMenuAll
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        //ImageCollection imageCollection;
        //List<Inventec.Desktop.Common.Modules.Module> modules;
        MenuAllADO menuAllADO;

        internal MenuAllBehavitor()
            : base()
        {
        }

        public MenuAllBehavitor(CommonParam param, object[] data)
            : base()
        {
            this.entity = data;
            //this.Module = module;
            //entity = data;
        }

        object IMenuAll.Run()
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
                            Module = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        
                        else if (item is MenuAllADO)
                        {
                            menuAllADO = (MenuAllADO)item;
                        }
                    }

                    if (Module != null && menuAllADO != null)
                    {
                        result = new FormMenuAll(Module, menuAllADO);                        
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
