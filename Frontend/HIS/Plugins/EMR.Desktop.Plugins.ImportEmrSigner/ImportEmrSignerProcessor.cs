using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.ImportEmrSigner
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "EMR.Desktop.Plugins.ImportEmrSigner",
           "Nhập khẩu tài khoản ký",
           "Common",
           16,
           "xlsx.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class ImportEmrSignerProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImportEmrSignerProcessor()
        {
            param = new CommonParam();
        }
        public ImportEmrSignerProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ImportEmrSigner.IImportEmrSigner behavior = ImportEmrSigner.ImportEmrSignerFactory.MakeIImportEmrSigner(param, args);
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
            return true;
        }
    }
}
