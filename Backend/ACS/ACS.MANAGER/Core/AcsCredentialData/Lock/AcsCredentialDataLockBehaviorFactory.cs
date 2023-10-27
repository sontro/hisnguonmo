using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Lock
{
    class AcsCredentialDataLockBehaviorFactory
    {
        internal static IAcsCredentialDataChangeLock MakeIAcsCredentialDataChangeLock(CommonParam param, object data)
        {
            IAcsCredentialDataChangeLock result = null;
            try
            {
                if (data.GetType() == typeof(ACS_CREDENTIAL_DATA))
                {
                    result = new AcsCredentialDataLockBehaviorEv(param, (ACS_CREDENTIAL_DATA)data);
                }
                if (data.GetType() == typeof(List<ACS_CREDENTIAL_DATA>))
                {
                    result = new AcsCredentialDataLockBehaviorListEv(param, (List<ACS_CREDENTIAL_DATA>)data);
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
