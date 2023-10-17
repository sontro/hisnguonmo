using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Get.ListV
{
    class SdaHideControlGetListVBehaviorFactory
    {
        internal static ISdaHideControlGetListV MakeISdaHideControlGetListV(CommonParam param, object data)
        {
            ISdaHideControlGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SdaHideControlViewFilterQuery))
                {
                    result = new SdaHideControlGetListVBehaviorByViewFilterQuery(param, (SdaHideControlViewFilterQuery)data);
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
