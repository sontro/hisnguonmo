using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedMapView
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.BedMapView",
        "Bản đồ giường",
        "Common",
        62,
        "newitem_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class BedMapViewProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BedMapViewProcessor()
        {
            param = new CommonParam();
        }

        public BedMapViewProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                BedMapView.IBedMapView behavior = BedMapView.BedMapViewFactory.MakeIBedMapView(param, args);
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
