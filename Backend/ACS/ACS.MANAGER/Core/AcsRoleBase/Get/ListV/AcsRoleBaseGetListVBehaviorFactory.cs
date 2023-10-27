using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.ListV
{
    class AcsRoleBaseGetListVBehaviorFactory
    {
        internal static IAcsRoleBaseGetListV MakeIAcsRoleBaseGetListV(CommonParam param, object data)
        {
            IAcsRoleBaseGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsRoleBaseViewFilterQuery))
                {
                    result = new AcsRoleBaseGetListVBehaviorByViewFilterQuery(param, (AcsRoleBaseViewFilterQuery)data);
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
