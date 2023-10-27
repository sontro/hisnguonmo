using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtp.OtpVerify
{
    class AcsOtpVerifyBehaviorFactory
    {
        internal static IAcsOtpVerify MakeIAcsOtpVerify(CommonParam param, object data)
        {
            IAcsOtpVerify result = null;
            try
            {
                if (data.GetType() == typeof(OtpVerifySDO))
                {
                    result = new AcsOtpVerifyBehaviorEv(param, (OtpVerifySDO)data);
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
