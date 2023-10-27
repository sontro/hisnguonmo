using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControl.Delete
{
    class AcsControlDeleteBehaviorFactory
    {
        internal static IAcsControlDelete MakeIAcsControlDelete(CommonParam param, object data)
        {
            IAcsControlDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_CONTROL))
                {
                    result = new AcsControlDeleteBehaviorEv(param, (ACS_CONTROL)data);
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
