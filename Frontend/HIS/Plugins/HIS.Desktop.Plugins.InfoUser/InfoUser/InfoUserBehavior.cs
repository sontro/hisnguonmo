using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfoUser.InfoUser
{
    class InfoUserBehavior : Tool<IDesktopToolContext>, IInfoUser
    {
        object[] entity;
        string loginName;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        internal InfoUserBehavior()
            : base()
        {

        }

        internal InfoUserBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IInfoUser.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;

                        if (item is string)
                        {
                            loginName = (string)item;
                        }
                    }
                }

                if (moduleData != null && !string.IsNullOrEmpty(loginName))
                {
                    return new frmInfoUser(moduleData, loginName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
