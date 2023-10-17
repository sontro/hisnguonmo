using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap.Get.ListEv
{
    class SdaCommuneMapGetListEvBehaviorFactory
    {
        internal static ISdaCommuneMapGetListEv MakeISdaCommuneMapGetListEv(CommonParam param, object data)
        {
            ISdaCommuneMapGetListEv result = null;
            try
            {
                if (data.GetType() == typeof(SdaCommuneMapFilterQuery))
                {
                    result = new SdaCommuneMapGetListEvBehaviorByFilterQuery(param, (SdaCommuneMapFilterQuery)data);
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
