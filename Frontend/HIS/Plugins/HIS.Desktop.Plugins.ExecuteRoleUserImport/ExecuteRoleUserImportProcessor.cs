using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.ExecuteRoleUserImport.ExecuteRoleUserImport;

namespace HIS.Desktop.Plugins.ExecuteRoleUserImport
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExecuteRoleUserImport",
       "Danh mục",
       "Bussiness",
       4,
       "tai-khoan.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class ExecuteRoleUserImportProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExecuteRoleUserImportProcessor()
        {
            param = new CommonParam();
        }
        public ExecuteRoleUserImportProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IExecuteRoleUserImport behavior = ExecuteRoleUserImportFactory.MakeIExecuteRoleUserImport(param, args);
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
