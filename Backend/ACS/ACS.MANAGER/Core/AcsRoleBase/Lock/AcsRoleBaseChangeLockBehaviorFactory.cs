using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Lock
{
    class AcsRoleBaseChangeLockBehaviorFactory
    {
        internal static IAcsRoleBaseChangeLock MakeIAcsRoleBaseChangeLock(CommonParam param, object data)
        {
            IAcsRoleBaseChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE_BASE))
                {
                    result = new AcsRoleBaseChangeLockBehaviorEv(param, (ACS_ROLE_BASE)data);
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
