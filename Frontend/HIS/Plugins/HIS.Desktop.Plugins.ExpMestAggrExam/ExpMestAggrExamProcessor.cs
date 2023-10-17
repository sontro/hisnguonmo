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
using HIS.Desktop.Plugins.ExpMestAggrExam.ExpMestAggrExam;

namespace HIS.Desktop.Plugins.ExpMestAggrExam
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExpMestAggrExam",
       "Tổng hợp phiếu lĩnh",
       "Common",
       14,
       "phieu-linh.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true
       )
    ]
    public class ExpMestAggrExamProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestAggrExamProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestAggrExamProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestAggrExam behavior = ExpMestAggrExamFactory.MakeIExpMestAggregate(param, args);
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
                result = true;
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
