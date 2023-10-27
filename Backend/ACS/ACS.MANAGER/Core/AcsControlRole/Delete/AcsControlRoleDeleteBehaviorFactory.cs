using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControlRole.Delete
{
    class AcsControlRoleDeleteBehaviorFactory
    {
        internal static IAcsControlRoleDelete MakeIAcsControlRoleDelete(CommonParam param, object data)
        {
            IAcsControlRoleDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_CONTROL_ROLE))
                {
                    result = new AcsControlRoleDeleteBehaviorEv(param, (ACS_CONTROL_ROLE)data);
                }
                else if (data.GetType() == typeof(List<long>))
                {
                    result = new AcsControlRoleDeleteListBehavior(param, (List<long>)data);
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
