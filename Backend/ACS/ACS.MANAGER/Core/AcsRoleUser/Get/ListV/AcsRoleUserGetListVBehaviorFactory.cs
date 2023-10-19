using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListV
{
    class AcsRoleUserGetListVBehaviorFactory
    {
        internal static IAcsRoleUserGetListV MakeIAcsRoleUserGetListV(CommonParam param, object data)
        {
            IAcsRoleUserGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsRoleUserViewFilterQuery))
                {
                    result = new AcsRoleUserGetListVBehaviorByViewFilterQuery(param, (AcsRoleUserViewFilterQuery)data);
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
