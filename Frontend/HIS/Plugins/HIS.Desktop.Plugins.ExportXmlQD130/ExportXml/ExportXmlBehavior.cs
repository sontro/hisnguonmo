using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130.ExportXml
{
    class ExportXmlBehavior : Tool<IDesktopToolContext>, IExportXml
    {
        object[] entity;
        internal ExportXmlBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExportXml.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Length > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = item as Inventec.Desktop.Common.Modules.Module;
                        }
                    }
                }
                if (WorkPlace.GetBranchId() > 0 && moduleData != null)
                {
                    return new UCExportXml(moduleData);
                }
                throw new NullReferenceException("BranchId<=0 OR moduleData = null");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
