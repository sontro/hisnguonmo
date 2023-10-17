using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.DebateDiagnostic.DebateDiagnostic;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.DebateDiagnostic
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.DebateDiagnostic",
          "Biên bản hội chẩn",
          "Common",
          62,
          "highlightfield_16x16.png",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)]
    public class DebateDiagnosticProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public DebateDiagnosticProcessor()
        {
            param = new CommonParam();
        }

        public DebateDiagnosticProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IDebateDiagnostic behavior = DebateDiagnosticFactory.MakeIDebateDiagnostic(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public override bool IsEnable()
        {
            return false;
        }
    }
}
