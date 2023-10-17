using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExamList
{
    class VaccinationExamListBehavior : Tool<IDesktopToolContext>, IVaccinationExamList
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        internal VaccinationExamListBehavior()
            : base()
        {

        }

        internal VaccinationExamListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IVaccinationExamList.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new frmVaccinationExamList(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
