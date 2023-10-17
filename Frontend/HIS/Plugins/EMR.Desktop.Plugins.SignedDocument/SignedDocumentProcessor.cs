using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace EMR.Desktop.Plugins.SignedDocument
{
    class SignedDocumentProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "EMR.Desktop.Plugins.SignedDocument",
           "",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class SignedDocumentSProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public SignedDocumentSProcessor()
            {
                param = new CommonParam();
            }
            public SignedDocumentSProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    SignedDocument.ISignedDocument behavior = SignedDocument.SignedDocumentFactory.MakeISignedDocument(param, args);
                    result = behavior != null ? (object)(behavior.Run()) : null;
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
                bool result = false;
                try
                {
                    result = true;
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                    result = false;
                }
                return result;
            }
        }
    }
}
