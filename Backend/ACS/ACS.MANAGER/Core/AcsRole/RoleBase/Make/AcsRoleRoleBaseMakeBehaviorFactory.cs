using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Make
{
    class AcsRoleRoleBaseMakeBehaviorFactory
    {
        internal static IAcsRoleRoleBaseMake MakeIAcsRoleRoleBaseMake(CommonParam param, object data)
        {
            IAcsRoleRoleBaseMake result = null;
            try
            {
                if (data.GetType() == typeof(AcsRoleSDO))
                {
                    result = new AcsRoleRoleBaseMakeBehavior(param, (AcsRoleSDO)data);
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
