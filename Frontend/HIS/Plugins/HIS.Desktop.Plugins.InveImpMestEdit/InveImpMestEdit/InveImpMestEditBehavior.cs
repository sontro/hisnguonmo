using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InveImpMestEdit.InveImpMestEdit
{
    class InveImpMestEditBehavior : Tool<IDesktopToolContext>, IInveImpMestEdit
    {
        object[] entity;

        internal InveImpMestEditBehavior()
            : base()
        { }

        internal InveImpMestEditBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IInveImpMestEdit.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long impMestId = 0;
                HIS.Desktop.Common.RefeshReference RefeshReference = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            impMestId = (long)entity[i];
                        }
                        else if (entity[i] is HIS.Desktop.Common.RefeshReference)
                        {
                            RefeshReference = (HIS.Desktop.Common.RefeshReference)entity[i];
                        }
                    }
                }
                if (moduleData != null && impMestId > 0)
                {
                    return new FormInveImpMestEdit(moduleData, impMestId, RefeshReference);
                }
                else
                {
                    return null;
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
