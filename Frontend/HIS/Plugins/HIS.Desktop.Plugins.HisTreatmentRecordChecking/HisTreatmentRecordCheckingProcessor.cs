using HIS.Desktop.Plugins.HisTreatmentRecordChecking.HisTreatmentRecordChecking;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTreatmentRecordChecking
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisTreatmentRecordChecking",
       "Tra soát hố sơ bệnh án",
       "Common",
       16,
       "",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class HisTreatmentRecordCheckingProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisTreatmentRecordCheckingProcessor()
        {
            param = new CommonParam();
        }
        public HisTreatmentRecordCheckingProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IHisTreatmentRecordChecking behavior = HisTreatmentRecordCheckingFactory.MakeIHisTreatmentRecordChecking(param, args);
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
