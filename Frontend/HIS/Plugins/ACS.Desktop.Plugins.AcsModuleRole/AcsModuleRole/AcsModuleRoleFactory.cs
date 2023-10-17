using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;

namespace ACS.Desktop.Plugins.AcsModuleRole.AcsModuleRole
{
    class AcsModuleRoleFactory
    {
        internal static IAcsModuleRole MakeIAcsModuleRole(CommonParam param, object[] data)
        {
            IAcsModuleRole result = null;
            try
            {
                result = new AcsModuleRoleBehavior(param, data);
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
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
