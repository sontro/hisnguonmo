using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.HisExpMestLaboratory.HisExpMestLaboratory
{
    class HisExpMestLaboratoryBehavior : Tool<IDesktopToolContext>, IHisExpMestLaboratory
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public HisExpMestLaboratoryBehavior()
            : base()
        {
        }

        public HisExpMestLaboratoryBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisExpMestLaboratory.Run()
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
                    }
                    if (currentModule != null)
                    {
                        result = new FormLaboratory(currentModule);
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
