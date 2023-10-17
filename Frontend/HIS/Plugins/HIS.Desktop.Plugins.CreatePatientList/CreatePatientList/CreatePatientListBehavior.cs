using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CreatePatientList.CreatePatientList
{
    class CreatePatientListBehavior : Tool<IDesktopToolContext>, ICreatePatientList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal CreatePatientListBehavior()
            : base()
        {

        }

        internal CreatePatientListBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ICreatePatientList.Run()
        {
            object result = null;
            try
            {
				Inventec.Desktop.Common.Modules.Module module = null;
                if (entity != null && entity.Count() > 0)
                {
					foreach (var item in entity)
					{
						if (item is Inventec.Desktop.Common.Modules.Module)
						{
							module = (Inventec.Desktop.Common.Modules.Module)item;
						}
					}
                    result = new frmCreatePatientList(module);
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
