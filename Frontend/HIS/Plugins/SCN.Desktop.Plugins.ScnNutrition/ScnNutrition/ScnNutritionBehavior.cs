using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCN.Desktop.Plugins.ScnNutrition
{
    class ScnNutritionBehavior : BusinessBase, IScnNutrition
    {
        object[] entity;
        internal ScnNutritionBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IScnNutrition.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                string personCode = "";
                object result = null;

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
                            if (entity[i] is string)
                            {
                                personCode = (string)entity[i];
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(personCode) && moduleData != null)
                    result = new frmScnNutrition(moduleData, personCode);

                return result;
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
