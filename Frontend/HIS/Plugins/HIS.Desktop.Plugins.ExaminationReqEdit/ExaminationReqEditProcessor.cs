using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExaminationReqEdit.ExaminationReqEdit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ExaminationReqEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExaminationReqEdit",
       "Sửa yêu cầu khám",
       "Common",
       62,
       "phong.png",
       "A",
       Module.MODULE_TYPE_ID__COMBO,
       true,
       true)]
    public class ExaminationReqEditProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExaminationReqEditProcessor()
        {
            param = new CommonParam();
        }
        public ExaminationReqEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExaminationReqEdit behavior = ExaminationReqEditFactory.MakeIExaminationReqEdit(param, args);
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
