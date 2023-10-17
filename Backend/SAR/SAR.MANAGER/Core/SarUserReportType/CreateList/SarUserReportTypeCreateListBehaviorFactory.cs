using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.CreateList
{
    class SarUserReportTypeCreateListBehaviorFactory
    {
        internal static ISarUserReportTypeCreateList MakeISarUserReportTypeCreateList(CommonParam param, object data)
        {
            ISarUserReportTypeCreateList result = null;
            try
            {
                if (data.GetType() == typeof(List<SAR_USER_REPORT_TYPE>))
                {
                    result = new SarUserReportTypeCreateListBehaviorEv(param, (List<SAR_USER_REPORT_TYPE>)data);
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
