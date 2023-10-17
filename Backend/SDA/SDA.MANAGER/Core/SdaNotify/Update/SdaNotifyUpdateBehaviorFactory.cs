using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Update
{
    class SdaNotifyUpdateBehaviorFactory
    {
        internal static ISdaNotifyUpdate MakeISdaNotifyUpdate(CommonParam param, object data)
        {
            ISdaNotifyUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_NOTIFY))
                {
                    result = new SdaNotifyUpdateBehaviorEv(param, (SDA_NOTIFY)data);
                }
                else if (data.GetType() == typeof(SDA.SDO.SdaNotifySeenSDO))
                {
                    result = new SdaNotifyUpdateLoginNamesBehaviorEv(param, (SDA.SDO.SdaNotifySeenSDO)data);
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
