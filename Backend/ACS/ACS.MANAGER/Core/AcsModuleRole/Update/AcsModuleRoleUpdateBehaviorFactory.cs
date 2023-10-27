using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole.Update
{
    class AcsModuleRoleUpdateBehaviorFactory
    {
        internal static IAcsModuleRoleUpdate MakeIAcsModuleRoleUpdate(CommonParam param, object data)
        {
            IAcsModuleRoleUpdate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_MODULE_ROLE))
                {
                    result = new AcsModuleRoleUpdateBehaviorEv(param, (ACS_MODULE_ROLE)data);
                }
                if (data.GetType() == typeof(List<ACS_MODULE_ROLE>))
                {
                    result = new AcsModuleRoleUpdateListBehavior(param, (List<ACS_MODULE_ROLE>)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
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
