using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Update
{
    class AcsRoleRoleBaseUpdateBehaviorFactory
    {
        internal static IAcsRoleRoleBaseUpdate MakeIAcsRoleRoleBaseUpdate(CommonParam param, object data)
        {
            IAcsRoleRoleBaseUpdate result = null;
            try
            {
                if (data.GetType() == typeof(AcsRoleSDO))
                {
                    result = new AcsRoleRoleBaseUpdateBehavior(param, (AcsRoleSDO)data);
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
