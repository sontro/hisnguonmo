using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportServiceRetyCat
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisImportServiceRetyCat",
           "Nhập khẩu loại máu",
           "Common",
           16,
           "xlsx.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class HisImportServiceRetyCatProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisImportServiceRetyCatProcessor()
        {
            param = new CommonParam();
        }
        public HisImportServiceRetyCatProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                HisImportServiceRetyCat.IHisImportServiceRetyCat behavior = HisImportServiceRetyCat.HisImportServiceRetyCatFactory.MakeIHisImportServiceRetyCat(param, args);
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
