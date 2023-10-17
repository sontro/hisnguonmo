using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.ReportTypeList.ReportTypeList
{
    class ReportTypeListFactory
    {
        internal static IReportTypeList MakeIReportTypeList(CommonParam param, object data)
        {
            IReportTypeList result = null;
            try
            {
                if (data.GetType() == typeof(object[]))
                {
                    result = new ReportTypeListBehavior(param, (object[])data);
                }
               
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
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
