using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication.Create
{
    class AcsApplicationCreateBehaviorFactory
    {
        internal static IAcsApplicationCreate MakeIAcsApplicationCreate(CommonParam param, object data)
        {
            IAcsApplicationCreate result = null;
            try
            {
                if (data.GetType() == typeof(AcsApplicationWithDataSDO))
                {
                    result = new AcsApplicationCreateSdoBehavior(param, (AcsApplicationWithDataSDO)data);
                }
                else if (data.GetType() == typeof(ACS_APPLICATION))
                {
                    result = new AcsApplicationCreateBehaviorEv(param, (ACS_APPLICATION)data);
                }
                else if (data.GetType() == typeof(List<ACS_APPLICATION>))
                {
                    result = new AcsApplicationCreateBehaviorListEv(param, (List<ACS_APPLICATION>)data);
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
