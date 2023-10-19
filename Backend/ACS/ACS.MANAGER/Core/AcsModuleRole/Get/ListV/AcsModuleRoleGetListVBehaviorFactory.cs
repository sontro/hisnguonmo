using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.ListV
{
    class AcsModuleRoleGetListVBehaviorFactory
    {
        internal static IAcsModuleRoleGetListV MakeIAcsModuleRoleGetListV(CommonParam param, object data)
        {
            IAcsModuleRoleGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsModuleRoleViewFilterQuery))
                {
                    result = new AcsModuleRoleGetListVBehaviorByViewFilterQuery(param, (AcsModuleRoleViewFilterQuery)data);
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
