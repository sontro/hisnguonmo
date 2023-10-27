using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys.Authentication
{
    class TokenSysAuthenticationResourceBehaviorFactory
    {
        internal static ITokenSysAuthenticationResource MakeIAcsTokenAuthentication(CommonParam param, object data)
        {
            ITokenSysAuthenticationResource result = null;
            try
            {
                if (data.GetType() == typeof(TokenSysAuthenticationResourceSDO))
                {
                    result = new TokenSysAuthenticationResourceBehavior(param, (TokenSysAuthenticationResourceSDO)data);
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
