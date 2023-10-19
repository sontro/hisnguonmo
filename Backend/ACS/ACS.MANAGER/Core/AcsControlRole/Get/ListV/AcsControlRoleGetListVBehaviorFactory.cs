using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Get.ListV
{
    class AcsControlRoleGetListVBehaviorFactory
    {
        internal static IAcsControlRoleGetListV MakeIAcsControlRoleGetListV(CommonParam param, object data)
        {
            IAcsControlRoleGetListV result = null;
            try
            {
                if (data.GetType() == typeof(AcsControlRoleViewFilterQuery))
                {
                    result = new AcsControlRoleGetListVBehaviorByViewFilterQuery(param, (AcsControlRoleViewFilterQuery)data);
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
