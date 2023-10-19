using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType.Delete
{
    class AcsActivityTypeDeleteBehaviorFactory
    {
        internal static IAcsActivityTypeDelete MakeIAcsActivityTypeDelete(CommonParam param, object data)
        {
            IAcsActivityTypeDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ACTIVITY_TYPE))
                {
                    result = new AcsActivityTypeDeleteBehaviorEv(param, (ACS_ACTIVITY_TYPE)data);
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
