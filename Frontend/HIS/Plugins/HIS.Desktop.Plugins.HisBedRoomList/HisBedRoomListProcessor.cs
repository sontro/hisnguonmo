using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.HisBedRoomList.HisBedRoomList;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBedRoomList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisBedRoomList",
        "Danh mục buồng bệnh",
        "Common",
        57,
        "buong-benh.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class HisBedRoomListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisBedRoomListProcessor()
        {
            param = new CommonParam();
        }
        public HisBedRoomListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisBedRoomList behavior = HisBedRoomListFactory.MakeIHisBedRoomList(param, args);
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
            bool result = false;
            try
            {
                if (GlobalVariables.CurrentRoomTypeCode == SdaConfigs.Get<string>(Inventec.Common.LocalStorage.SdaConfig.ConfigKeys.DBCODE__HIS_RS__HIS_ROOM_TYPE__ROOM_TYPE_CODE__RECEPTION))
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
