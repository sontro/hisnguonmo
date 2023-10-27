using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplication.Delete
{
    class AcsApplicationDeleteBehaviorFactory
    {
        internal static IAcsApplicationDelete MakeIAcsApplicationDelete(CommonParam param, object data)
        {
            IAcsApplicationDelete result = null;
            try
            {
                if (data.GetType() == typeof(ACS_APPLICATION))
                {
                    result = new AcsApplicationDeleteBehaviorEv(param, (ACS_APPLICATION)data);
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
