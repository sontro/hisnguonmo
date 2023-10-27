using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys.Authorize
{
    class AcsTokenAuthorizeBehaviorFactory
    {
        internal static IAcsTokenAuthorize MakeIAcsTokenLogin(CommonParam param, object data)
        {
            IAcsTokenAuthorize result = null;
            try
            {
                if (data.GetType() == typeof(AcsTokenLoginSDO))
                {
                    if (data != null && !String.IsNullOrEmpty(((AcsTokenLoginSDO)data).AUTHOR_SYSTEM_CODE))
                    {
                        result = new TokenSysAuthorizeHasAuthorSystemBehavior(param, (AcsTokenLoginSDO)data);
                    }
                    else
                        result = new AcsTokenAuthorizeBehavior(param, (AcsTokenLoginSDO)data);
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
