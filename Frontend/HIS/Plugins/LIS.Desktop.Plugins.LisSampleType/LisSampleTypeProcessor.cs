using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;
using LIS.Desktop.Plugins.LisSampleType.LisSampleType;

namespace LIS.Desktop.Plugins.LisSampleType
{
    
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "LIS.Desktop.Plugins.LisSampleType",
    "Loại mẫu bệnh phẩm",
    "Common",
    14,
    "mai-benh-pham.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true
    )
    ]

    public class LisSampleTypeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisSampleTypeProcessor()
        {
            param = new CommonParam();
        }
        public LisSampleTypeProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILisSampleType behavior = LisSampleTypeFactory.MakeIControl(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Ham tra ve trang thai cua module la enable hay disable
        /// Ghi de gia tri khac theo nghiep vu tung module
        /// </summary>
        /// <returns>true/false</returns>
        public override bool IsEnable()
        {
            bool result = false;
            try
            {
                if (GlobalVariables.CurrentRoomTypeCodes.Contains(SdaConfigs.Get<string>(Inventec.Common.LocalStorage.SdaConfig.ConfigKeys.DBCODE__HIS_RS__HIS_ROOM_TYPE__ROOM_TYPE_CODE__RECEPTION)))
                {
                    result = true;
                }
                else
                    result = false;
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
