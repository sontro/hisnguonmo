using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineGeneral.PublicMedicineGeneral
{
    class PublicMedicineGeneralBehavior : Tool<IDesktopToolContext>, IPublicMedicineGeneral
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public PublicMedicineGeneralBehavior()
            : base()
        {
        }

        public PublicMedicineGeneralBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IPublicMedicineGeneral.Run()
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
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        if (currentModule != null)
                        {
                            result = new FormPublicMedicineGeneral(currentModule);
                            break;
                        }
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
