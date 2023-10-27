using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole.Create
{
    class AcsApplicationRoleCreateBehaviorFactory
    {
        internal static IAcsApplicationRoleCreate MakeIAcsApplicationRoleCreate(CommonParam param, object data)
        {
            IAcsApplicationRoleCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_APPLICATION_ROLE))
                {
                    result = new AcsApplicationRoleCreateBehaviorEv(param, (ACS_APPLICATION_ROLE)data);
                }
                else if (data.GetType() == typeof(List<ACS_APPLICATION_ROLE>))
                {
                    result = new AcsApplicationRoleCreateBehaviorListEv(param, (List<ACS_APPLICATION_ROLE>)data);
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
