using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole.Delete
{
    class AcsApplicationRoleDeleteBehaviorFactory
    {
        internal static IAcsApplicationRoleDelete MakeIAcsApplicationRoleDelete(CommonParam param, object data)
        {
            IAcsApplicationRoleDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_APPLICATION_ROLE))
                {
                    result = new AcsApplicationRoleDeleteBehaviorEv(param, (ACS_APPLICATION_ROLE)data);
                }
                else if (data.GetType() == typeof(List<long>))
                {
                    result = new AcsApplicationRoleDeleteBehaviorListEv(param, (List<long>)data);
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
