using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAR.SDO;

namespace SAR.MANAGER.Core.SarUserReportType.Copy
{
    class SarUserReportTypeCopyFactory
    {
        internal static ISarUserReportTypeCopy MakeISarUserReportTypeCopy(CommonParam param, object data)
        {
            ISarUserReportTypeCopy result = null;
            try
            {
                if (data.GetType() == typeof(SarUserReportTypeCopyByUserSDO))
                {
                    result = new SarUserReportTypeCopyByUserBehavior(param, (SarUserReportTypeCopyByUserSDO)data);
                }
                else if (data.GetType() == typeof(SarUserReportTypeCopyByReportTypeSDO))
                {
                    result = new SarUserReportTypeCopyByReportTypeBehavior(param, (SarUserReportTypeCopyByReportTypeSDO)data);
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
