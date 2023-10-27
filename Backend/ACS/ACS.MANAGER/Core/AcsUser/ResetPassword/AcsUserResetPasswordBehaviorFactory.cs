using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.ResetPassword
{
    class AcsUserResetPasswordBehaviorFactory
    {
        internal static IAcsUserResetPassword MakeIAcsUserResetPassword(CommonParam param, object data)
        {
            IAcsUserResetPassword result = null;
            try
            {
                if (data.GetType() == typeof(ACS_USER))
                {
                    result = new AcsUserResetPasswordBehaviorEv(param, (ACS_USER)data);
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
