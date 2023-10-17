using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentIcdEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.TreatmentIcdEdit",
           "Sửa thông tin hồ sơ điều trị",
           "Common",
           16,
           "morefunctions_32x32.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class TreatmentIcdEditProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TreatmentIcdEditProcessor()
        {
            param = new CommonParam();
        }
        public TreatmentIcdEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                TreatmentIcdEdit.ITreatmentIcdEdit behavior = TreatmentIcdEdit.TreatmentIcdEditFactory.MakeITreatmentIcdEdit(param, args);
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
