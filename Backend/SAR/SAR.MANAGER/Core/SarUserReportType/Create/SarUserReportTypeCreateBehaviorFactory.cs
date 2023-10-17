using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.Create
{
    class SarUserReportTypeCreateBehaviorFactory
    {
        internal static ISarUserReportTypeCreate MakeISarUserReportTypeCreate(CommonParam param, object data)
        {
            ISarUserReportTypeCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_USER_REPORT_TYPE))
                {
                    result = new SarUserReportTypeCreateBehaviorEv(param, (SAR_USER_REPORT_TYPE)data);
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
