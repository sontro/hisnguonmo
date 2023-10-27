using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.Create
{
    class AcsRoleCreateBehaviorFactory
    {
        internal static IAcsRoleCreate MakeIAcsRoleCreate(CommonParam param, object data)
        {
            IAcsRoleCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ROLE))
                {
                    result = new AcsRoleCreateBehaviorEv(param, (ACS_ROLE)data);
                }
                else if (data.GetType() == typeof(List<ACS_ROLE>))
                {
                    result = new AcsRoleCreateBehaviorListEv(param, (List<ACS_ROLE>)data);
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
