using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisPatient.HisPatient;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.HisPatient
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisPatient",
           "Danh sách máu",
           "Common",
           16,
           "thuoc.png",
           "E",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
    ]
    public class HisPatientProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisPatientProcessor()
        {
            param = new CommonParam();
        }
        public HisPatientProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IHisPatient behavior = HisPatientFactory.MakeIHisPatient(param, args);
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
