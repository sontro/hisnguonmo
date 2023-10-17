using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.MaterialTypeCreateParent.MaterialTypeCreateParent
{
    class MaterialTypeCreateParentBehavior:BusinessBase, IMaterialTypeCreateParent
    {
         object[] entity;
         internal MaterialTypeCreateParentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IMaterialTypeCreateParent.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData _delegateSelect = null;
                MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE meterial = null;
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
                            if (entity[i] is DelegateSelectData)
                            {
                                _delegateSelect = (DelegateSelectData)entity[i];
                            }
                            if (entity[i] is V_HIS_MATERIAL_TYPE)
                            {
                                meterial = (V_HIS_MATERIAL_TYPE)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && meterial != null)
                {
                    return new frmMaterialTypeCreateParent(moduleData, _delegateSelect, meterial);
                }
                else
                return new frmMaterialTypeCreateParent(moduleData, _delegateSelect);
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