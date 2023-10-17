using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoleUser.ExecuteRoleUser
{
    class ExecuteRoleUserBehavior : Tool<IDesktopToolContext>, IExecuteRoleUser
    {
        object[] entity;

        internal ExecuteRoleUserBehavior()
            : base()
        {

        }

        internal ExecuteRoleUserBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExecuteRoleUser.Run()
        {
            object result = null;
            Inventec.Desktop.Common.Modules.Module currentModule = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {

                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                }
                result = new UC_ExecuteRoleUser(currentModule);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
