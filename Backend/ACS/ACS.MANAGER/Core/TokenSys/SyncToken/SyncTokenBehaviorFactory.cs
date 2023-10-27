using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.TokenSys.SyncToken
{
    class AcsTokenSyncBehaviorFactory
    {
        internal static IAcsTokenSync MakeIAcsTokenLogin(CommonParam param, object data)
        {
            IAcsTokenSync result = null;
            try
            {
                if (data.GetType() == typeof(List<AcsCredentialTrackingSDO>))
                {
                    result = new AcsTokenSyncBehavior(param, (List<AcsCredentialTrackingSDO>)data);
                }
                else if (data.GetType() == typeof(AcsTokenSyncInsertSDO))
                {
                    result = new SyncTokenInsertBehavior(param, (AcsTokenSyncInsertSDO)data);
                }
                else if (data.GetType() == typeof(AcsTokenSyncDeleteSDO))
                {
                    result = new SyncTokenDeleteBehavior(param, (AcsTokenSyncDeleteSDO)data);
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
