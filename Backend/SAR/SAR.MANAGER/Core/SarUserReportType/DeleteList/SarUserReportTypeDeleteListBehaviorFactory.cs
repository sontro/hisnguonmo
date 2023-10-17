using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.DeleteList
{
    class SarUserReportTypeDeleteListBehaviorFactory
    {
        internal static ISarUserReportTypeDeleteList MakeISarUserReportTypeDeleteList(CommonParam param, object data)
        {
            ISarUserReportTypeDeleteList result = null;
            try
            {
                if (data.GetType() == typeof(List<long>))
                {
                    result = new SarUserReportTypeDeleteListBehaviorEv(param, (List<long>)data);
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
