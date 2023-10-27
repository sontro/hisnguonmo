using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser.CheckResetPasswordB
{
    class AcsUserCheckResetPasswordBBehaviorFactory
    {
        internal static IAcsUserCheckResetPasswordB MakeIAcsUserCheckResetPasswordB(CommonParam param, object data)
        {
            IAcsUserCheckResetPasswordB result = null;
            try
            {
                if (data.GetType() == typeof(AcsUserCheckResetPasswordTDO))
                {
                    result = new AcsUserCheckResetPasswordBBehaviorEv(param, (AcsUserCheckResetPasswordTDO)data);
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
