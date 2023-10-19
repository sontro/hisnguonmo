using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Get.ListV
{
    class AcsRoleGetListVBehaviorFactory
    {
        internal static IAcsRoleGetListV MakeIAcsRoleGetListV(CommonParam param, object data)
        {
            IAcsRoleGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsRoleViewFilterQuery))
                {
                    result = new AcsRoleGetListVBehaviorByViewFilterQuery(param, (AcsRoleViewFilterQuery)data);
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
