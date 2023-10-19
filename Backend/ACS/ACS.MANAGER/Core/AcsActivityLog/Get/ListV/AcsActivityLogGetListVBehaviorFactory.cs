using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.ListV
{
    class AcsActivityLogGetListVBehaviorFactory
    {
        internal static IAcsActivityLogGetListV MakeIAcsActivityLogGetListV(CommonParam param, object data)
        {
            IAcsActivityLogGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsActivityLogViewFilterQuery))
                {
                    result = new AcsActivityLogGetListVBehaviorByViewFilterQuery(param, (AcsActivityLogViewFilterQuery)data);
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
