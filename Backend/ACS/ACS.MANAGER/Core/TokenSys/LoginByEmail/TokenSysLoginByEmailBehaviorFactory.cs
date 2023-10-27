using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys.LoginByEmail
{
    class TokenSysLoginByEmailBehaviorFactory
    {
        internal static ITokenSysLoginByEmail MakeIAcsTokenLoginByEmail(CommonParam param, object data)
        {
            ITokenSysLoginByEmail result = null;
            try
            {
                if (data.GetType() == typeof(LoginByEmailTDO))
                {
                    result = new TokenSysLoginByEmailBehaviorEv(param, (LoginByEmailTDO)data);
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
