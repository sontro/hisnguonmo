using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.HisRationSumList.HisRationSumList;

namespace HIS.Desktop.Plugins.HisRationSumList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.HisRationSumList",
       "Danh mục",
       "Bussiness",
       4,
       "phe-duyet.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class HisRationSumListProcessor:ModuleBase,IDesktopRoot
    {
CommonParam param;
		public HisRationSumListProcessor()
        {
            param = new CommonParam();
        }
        public HisRationSumListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }        

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisRationSumList behavior = HisRationSumListFactory.MakeIRationSum(param, args);
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