using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.Vaccination.Vaccination
{
    class VaccinationBehavior : Tool<IDesktopToolContext>, IVaccination
    {
        object[] entity;
        internal VaccinationBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IVaccination.Run()
        {
            object rs = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_VACCINATION vaccination = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        if (entity[i] is V_HIS_VACCINATION)
                        {
                            vaccination = (V_HIS_VACCINATION)entity[i];
                        }
                    }
                }
                if (vaccination != null && moduleData != null)
                {
                    rs = new frmVaccination(moduleData, vaccination);
                }
                else if (vaccination == null && moduleData != null)
                {
                    rs = new UCVaccination(moduleData);
                }
                //return new UCVaccination();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
            return rs;
        }
    }
}
