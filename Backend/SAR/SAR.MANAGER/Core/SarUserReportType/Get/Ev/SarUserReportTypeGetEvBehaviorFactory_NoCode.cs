using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.Ev
{
    class SarUserReportTypeGetEvBehaviorFactory
    {
        internal static ISarUserReportTypeGetEv MakeISarUserReportTypeGetEv(CommonParam param, object data)
        {
            ISarUserReportTypeGetEv result = null;
            try
            {
                if (data.GetType() == typeof(long))
                {
                    result = new SarUserReportTypeGetEvBehaviorById(param, long.Parse(data.ToString()));
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
