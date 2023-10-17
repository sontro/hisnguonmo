using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SampleCollectionRoom;
using HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoom;

namespace HIS.Desktop.Plugins.SampleCollectionRoom
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SampleCollectionRoom",
        "Kết nối xét nghiệm",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class SampleCollectionRoomProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SampleCollectionRoomProcessor()
        {
            param = new CommonParam();
        }
        public SampleCollectionRoomProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ISampleCollectionRoom behavior = SampleCollectionRoomFactory.MakeIConnectionTest(param, args);
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
