using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.AssignNutritionEdit
{
    class AssignNutritionEditBehavior : BusinessBase, IAssignNutritionEdit
    {
        object[] entity;
        //DelegateRefreshData delegateRefresh = null;

        internal AssignNutritionEditBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAssignNutritionEdit.Run()
        {
            object frm = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                AssignServiceEditADO ado = null;


                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is AssignServiceEditADO)
                            {
                                ado = (AssignServiceEditADO)entity[i];
                            }                          
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                frm = new HIS.Desktop.Plugins.AssignNutritionEdit.Run.frmAssignNutritionEdit(moduleData, ado);

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
