using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Lock
{
    class AcsModuleChangeLockBehaviorFactory
    {
        internal static IAcsModuleChangeLock MakeIAcsModuleChangeLock(CommonParam param, object data)
        {
            IAcsModuleChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_MODULE))
                {
                    result = new AcsModuleChangeLockBehaviorEv(param, (ACS_MODULE)data);
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
