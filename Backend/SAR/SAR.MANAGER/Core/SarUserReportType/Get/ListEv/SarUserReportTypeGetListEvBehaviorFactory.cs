using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.ListEv
{
    class SarUserReportTypeGetListEvBehaviorFactory
    {
        internal static ISarUserReportTypeGetListEv MakeISarUserReportTypeGetListEv(CommonParam param, object data)
        {
            ISarUserReportTypeGetListEv result = null;
            try
            {
                if (data.GetType() == typeof(SarUserReportTypeFilterQuery))
                {
                    result = new SarUserReportTypeGetListEvBehaviorByFilterQuery(param, (SarUserReportTypeFilterQuery)data);
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
