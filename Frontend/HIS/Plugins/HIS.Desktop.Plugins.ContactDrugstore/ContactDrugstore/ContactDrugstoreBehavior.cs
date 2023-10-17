using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContactDrugstore.ContactDrugstore
{
    class ContactDrugstoreBehavior : Tool<IDesktopToolContext>, IContactDrugstore
    {
        long entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;

        internal ContactDrugstoreBehavior()
            : base()
        {

        }

        internal ContactDrugstoreBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            entity = data;
            moduleData = module;
        }

        object IContactDrugstore.Run()
        {
            object result = null;
            try
            {
                result = new frmContactDrugstore(moduleData, entity);
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
