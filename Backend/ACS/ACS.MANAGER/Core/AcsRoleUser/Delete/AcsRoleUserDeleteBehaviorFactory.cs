using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Delete
{
    class AcsRoleUserDeleteBehaviorFactory
    {
        internal static IAcsRoleUserDelete MakeIAcsRoleUserDelete(CommonParam param, object data)
        {
            IAcsRoleUserDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE_USER))
                {
                    result = new AcsRoleUserDeleteBehaviorEv(param, (ACS_ROLE_USER)data);
                }
                else if (data.GetType() == typeof(List<ACS_ROLE_USER>))
                {
                    result = new AcsRoleUserDeleteBehaviorListEv(param, (List<ACS_ROLE_USER>)data);
                }
                if (data.GetType() == typeof(List<long>))
                {
                    result = new AcsRoleUserDeleteBehaviorListId(param, (List<long>)data);
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
