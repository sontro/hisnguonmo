using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog.Create
{
    class SdaEventLogCreateBehaviorFactory
    {
        internal static ISdaEventLogCreate MakeISdaEventLogCreate(CommonParam param, object data)
        {
            ISdaEventLogCreate result = null;
            try
            {
                if (data.GetType() == typeof(SDA_EVENT_LOG))
                {
                    result = new SdaEventLogCreateBehaviorEv(param, (SDA_EVENT_LOG)data);
                }
                if (data.GetType() == typeof(SDA.SDO.SdaEventLogSDO))
                {
                    result = new SdaEventLogCreateBehaviorSDO(param, (SDA.SDO.SdaEventLogSDO)data);
                }
                if (data.GetType() == typeof(List<SDA.SDO.SdaEventLogSDO>))
                {
                    result = new SdaEventLogCreateListBehaviorSDO(param, (List<SDA.SDO.SdaEventLogSDO>)data);
                }
                else if (data.GetType() == typeof(List<SDA_EVENT_LOG>))
                {
                    result = new SdaEventLogCreateBehaviorListEv(param, (List<SDA_EVENT_LOG>)data);
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
