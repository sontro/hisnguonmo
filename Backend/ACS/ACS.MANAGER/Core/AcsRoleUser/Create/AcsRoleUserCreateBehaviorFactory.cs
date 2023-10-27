using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Create
{
    class AcsRoleUserCreateBehaviorFactory
    {
        internal static IAcsRoleUserCreate MakeIAcsRoleUserCreate(CommonParam param, object data)
        {
            IAcsRoleUserCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE_USER))
                {
                    result = new AcsRoleUserCreateBehaviorEv(param, (ACS_ROLE_USER)data);
                }
                else if (data.GetType() == typeof(List<ACS_ROLE_USER>))
                {
                    result = new AcsRoleUserCreateBehaviorListEv(param, (List<ACS_ROLE_USER>)data);
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
