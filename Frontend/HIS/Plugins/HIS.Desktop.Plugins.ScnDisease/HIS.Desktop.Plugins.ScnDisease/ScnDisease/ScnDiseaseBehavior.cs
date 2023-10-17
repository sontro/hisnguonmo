using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ScnDisease.ScnDisease
{
    class ScnDiseaseBehavior : BusinessBase, IScnDisease
    {
        object[] entity;
        internal ScnDiseaseBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IScnDisease.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                string _patientCode = "";
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
                            else if (entity[i] is string)
                            {
                                _patientCode = (string)entity[i];
                            }
                        }
                    }
                }

                return new frmScnDisease(moduleData, _patientCode);
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
