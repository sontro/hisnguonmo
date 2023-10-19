using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole.Delete
{
    class AcsModuleRoleDeleteBehaviorFactory
    {
        internal static IAcsModuleRoleDelete MakeIAcsModuleRoleDelete(CommonParam param, object data)
        {
            IAcsModuleRoleDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_MODULE_ROLE))
                {
                    result = new AcsModuleRoleDeleteBehaviorEv(param, (ACS_MODULE_ROLE)data);
                }
                if (data.GetType() == typeof(List<long>))
                {
                    result = new AcsModuleRoleDeleteListBehavior(param, (List<long>)data);
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
