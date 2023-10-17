using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSign.EmrSign
{
    class EmrSignBehavior : Tool<IDesktopToolContext>, IEmrSign
    {
        object[] entity;

        internal EmrSignBehavior()
            : base()
        { }

        internal EmrSignBehavior(Inventec.Core.CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IEmrSign.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            long documentId = 0;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is long)
                        {
                            documentId = (long)item;
                        }
                    }
                }

                if (moduleData != null)
                {
                    return new FormEmrSign(moduleData, documentId);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Error("documentId: " + documentId);
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
