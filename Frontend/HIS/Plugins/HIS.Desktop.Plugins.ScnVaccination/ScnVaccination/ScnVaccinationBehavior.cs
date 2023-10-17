using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ScnVaccination.Run;

namespace HIS.Desktop.Plugins.ScnVaccination.ScnVaccination
{
    class ScnVaccinationBehavior : BusinessBase, IScnVaccination
    {
        object[] entity;
        internal ScnVaccinationBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IScnVaccination.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                string _PatientCode = "";
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
                                _PatientCode = (string)entity[i];
                            }
                        }
                    }
                }

                return new frmScnVaccination(moduleData, _PatientCode);
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
