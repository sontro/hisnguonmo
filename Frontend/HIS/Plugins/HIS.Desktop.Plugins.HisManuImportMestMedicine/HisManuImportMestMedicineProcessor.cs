using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.HisManuImportMestMedicine.HisManuImportMestMedicine;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisManuImportMestMedicine
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisManuImportMestMedicine",
       "Danh sách nhập từ nhà cung cấp",
       "Common",
       16,
       "ManuImpMest_32x32.png",
       "B",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class HisManuImportMestMedicineProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisManuImportMestMedicineProcessor()
        {
            param = new CommonParam();
        }
        public HisManuImportMestMedicineProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IHisManuImportMestMedicine behavior = HisManuImportMestMedicineFactory.MakeIHisManuImportMestMedicine(param, args);
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
            //bool result = false;
            //try
            //{
            //    if (GlobalVariables.CurrentRoomTypeCode.Contains(SdaConfigs.Get<string>(Inventec.Common.LocalStorage.SdaConfig.ConfigKeys.DBCODE__HIS_RS__HIS_ROOM_TYPE__ROOM_TYPE_CODE__STOCK)))
            //    {
            //        result = true;
            //    }
            //    else
            //        result = false;
            //}
            //catch (Exception ex)
            //{
            //    Inventec.Common.Logging.LogSystem.Error(ex);
            //}
            return true;
        }
    }
}
