using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.ListPresBloodConfirm.ListPresBloodConfirm;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ListPresBloodConfirm
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ListPresBloodConfirm",
       "Danh sách nhập",
       "Common",
       16,
       "nhap-kho.png",     
       "A",
       Module.MODULE_TYPE_ID__COMBO,
       true,
       true)
    ]

    public class ListPresBloodConfirmProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ListPresBloodConfirmProcessor()
        {
            param = new CommonParam();
        }
        public ListPresBloodConfirmProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IListPresBloodConfirm behavior = ListPresBloodConfirmFactory.MakeIHisImportMestMedicine(param, args);
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
