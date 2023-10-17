using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Update
{
    class SarUserReportTypeUpdateBehaviorFactory
    {
        internal static ISarUserReportTypeUpdate MakeISarUserReportTypeUpdate(CommonParam param, object data)
        {
            ISarUserReportTypeUpdate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_USER_REPORT_TYPE))
                {
                    result = new SarUserReportTypeUpdateBehaviorEv(param, (SAR_USER_REPORT_TYPE)data);
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
