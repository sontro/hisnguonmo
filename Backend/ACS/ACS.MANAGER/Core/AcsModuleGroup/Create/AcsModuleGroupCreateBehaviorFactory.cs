using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleGroup.Create
{
    class AcsModuleGroupCreateBehaviorFactory
    {
        internal static IAcsModuleGroupCreate MakeIAcsModuleGroupCreate(CommonParam param, object data)
        {
            IAcsModuleGroupCreate result = null;
            try
            {
                if (data.GetType() == typeof(ACS_MODULE_GROUP))
                {
                    result = new AcsModuleGroupCreateBehaviorEv(param, (ACS_MODULE_GROUP)data);
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
