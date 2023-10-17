using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Get.ListEv
{
    class SdaDeleteDataGetListEvBehaviorFactory
    {
        internal static ISdaDeleteDataGetListEv MakeISdaDeleteDataGetListEv(CommonParam param, object data)
        {
            ISdaDeleteDataGetListEv result = null;
            try
            {
                if (data.GetType() == typeof(SdaDeleteDataFilterQuery))
                {
                    result = new SdaDeleteDataGetListEvBehaviorByFilterQuery(param, (SdaDeleteDataFilterQuery)data);
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
