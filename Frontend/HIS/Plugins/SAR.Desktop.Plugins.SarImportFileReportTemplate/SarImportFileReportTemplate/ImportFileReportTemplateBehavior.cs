using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Tools;
using Inventec.Desktop.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core.Actions;
using Inventec.Core;

namespace SAR.Desktop.Plugins.SarImportFileReportTemplate.SarImportFileReportTemplate
{
    class ImportFileReportTemplateBehavior:Tool<IDesktopToolContext>, IImportFileReportTemplate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        RefeshReference delegateRefresh;
        public ImportFileReportTemplateBehavior()
            : base()
        {
        }

        public ImportFileReportTemplateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImportFileReportTemplate.Run()
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
                        else if (item is RefeshReference)
                        {
                            delegateRefresh = (RefeshReference)item;
                        }
                    }
                    if (currentModule != null && delegateRefresh != null)
                    {
                        result = new frmSarImportFileReportTemplate(currentModule, delegateRefresh);
                    }
                    else
                    {
                        result = new frmSarImportFileReportTemplate(currentModule);
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
