using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsAppOtpType.Create
{
    class AcsAppOtpTypeCreateBehaviorFactory
    {
        internal static IAcsAppOtpTypeCreate MakeIAcsAppOtpTypeCreate(CommonParam param, object data)
        {
            IAcsAppOtpTypeCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_APP_OTP_TYPE))
                {
                    result = new AcsAppOtpTypeCreateBehaviorEv(param, (ACS_APP_OTP_TYPE)data);
                }
                if (data.GetType() == typeof(List<ACS_APP_OTP_TYPE>))
                {
                    result = new AcsAppOtpTypeCreateListBehavior(param, (List<ACS_APP_OTP_TYPE>)data);
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
