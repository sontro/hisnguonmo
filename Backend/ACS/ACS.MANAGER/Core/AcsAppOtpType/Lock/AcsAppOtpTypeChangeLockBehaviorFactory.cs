using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType.Lock
{
    class AcsAppOtpTypeChangeLockBehaviorFactory
    {
        internal static IAcsAppOtpTypeChangeLock MakeIAcsAppOtpTypeChangeLock(CommonParam param, object data)
        {
            IAcsAppOtpTypeChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_APP_OTP_TYPE))
                {
                    result = new AcsAppOtpTypeChangeLockBehaviorEv(param, (ACS_APP_OTP_TYPE)data);
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
