using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityLog.Create
{
    class AcsActivityLogCreateBehaviorFactory
    {
        internal static IAcsActivityLogCreate MakeIAcsActivityLogCreate(CommonParam param, object data)
        {
            IAcsActivityLogCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_ACTIVITY_LOG))
                {
                    result = new AcsActivityLogCreateBehaviorEv(param, (ACS_ACTIVITY_LOG)data);
                }
                else if (data.GetType() == typeof(List<ACS_ACTIVITY_LOG>))
                {
                    result = new AcsActivityLogCreateBehaviorListEv(param, (List<ACS_ACTIVITY_LOG>)data);
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
