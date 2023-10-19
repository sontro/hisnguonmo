using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleBase.Create
{
    class AcsRoleBaseCreateBehaviorFactory
    {
        internal static IAcsRoleBaseCreate MakeIAcsRoleBaseCreate(CommonParam param, object data)
        {
            IAcsRoleBaseCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE_BASE))
                {
                    result = new AcsRoleBaseCreateBehaviorEv(param, (ACS_ROLE_BASE)data);
                }
                else if (data.GetType() == typeof(List<ACS_ROLE_BASE>))
                {
                    result = new AcsRoleBaseCreateBehaviorListEv(param, (List<ACS_ROLE_BASE>)data);
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
