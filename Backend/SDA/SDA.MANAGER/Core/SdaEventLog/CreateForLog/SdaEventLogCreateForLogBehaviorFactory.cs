using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog.CreateForLog
{
    class SdaEventLogCreateForLogBehaviorFactory
    {
        internal static ISdaEventLogCreateForLog MakeISdaEventLogCreateForLog(CommonParam param, string description)
        {
            ISdaEventLogCreateForLog result = null;
            try
            {
                result = new SdaEventLogCreateBehaviorEv_ForLog(param, description);

                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => description), description), ex);
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
