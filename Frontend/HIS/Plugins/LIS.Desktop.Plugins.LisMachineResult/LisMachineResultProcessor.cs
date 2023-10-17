using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Common.Modules;
using LIS.Desktop.Plugins.LisMachineResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;

namespace LIS.Desktop.Plugins.LisMachineResult
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "LIS.Desktop.Plugins.LisMachineResult",
       "Danh mục",
       "Bussiness",
       4,
       "",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class LisMachineResultProcessor : ModuleBase, IDesktopRoot
    {
		CommonParam param;
		public LisMachineResultProcessor()
        {
            param = new CommonParam();
        }
        public LisMachineResultProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }        

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILisMachineResult behavior = LisMachineResultFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
