using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceAdd
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExamServiceAdd",
       "Khám thêm",
       "Common",
       62,
       "editcontact_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)]

    public class ExamServiceAddProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExamServiceAddProcessor()
        {
            param = new CommonParam();
        }
        public ExamServiceAddProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ExamServiceAdd.IExamServiceAdd behavior = ExamServiceAdd.ExamServiceAddFactory.MakeIExamServiceAdd(param, args);
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
