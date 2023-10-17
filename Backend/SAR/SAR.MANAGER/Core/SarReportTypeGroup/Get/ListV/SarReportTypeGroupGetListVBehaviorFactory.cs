using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.ListV
{
    class SarReportTypeGroupGetListVBehaviorFactory
    {
        internal static ISarReportTypeGroupGetListV MakeISarReportTypeGroupGetListV(CommonParam param, object data)
        {
            ISarReportTypeGroupGetListV result = null;
            try
            {
                if (data.GetType() == typeof(SarReportTypeGroupViewFilterQuery))
                {
                    result = new SarReportTypeGroupGetListVBehaviorByViewFilterQuery(param, (SarReportTypeGroupViewFilterQuery)data);
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
