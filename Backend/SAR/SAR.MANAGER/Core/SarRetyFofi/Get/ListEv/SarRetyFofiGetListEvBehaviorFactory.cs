using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.ListEv
{
    class SarRetyFofiGetListEvBehaviorFactory
    {
        internal static ISarRetyFofiGetListEv MakeISarRetyFofiGetListEv(CommonParam param, object data)
        {
            ISarRetyFofiGetListEv result = null;
            try
            {
                if (data.GetType() == typeof(SarRetyFofiFilterQuery))
                {
                    result = new SarRetyFofiGetListEvBehaviorByFilterQuery(param, (SarRetyFofiFilterQuery)data);
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
