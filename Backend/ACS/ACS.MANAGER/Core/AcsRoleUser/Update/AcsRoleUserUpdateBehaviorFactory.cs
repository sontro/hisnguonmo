using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Update
{
    class AcsRoleUserUpdateBehaviorFactory
    {
        internal static IAcsRoleUserUpdate MakeIAcsRoleUserUpdate(CommonParam param, object data)
        {
            IAcsRoleUserUpdate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE_USER))
                {
                    result = new AcsRoleUserUpdateBehaviorEv(param, (ACS_ROLE_USER)data);
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
