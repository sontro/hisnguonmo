
using HIS.Desktop.ADO;
using Inventec.Core;
using SAR.Desktop.Plugins.SarReportTemplate.SarReportTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportTemplate
{
    class SarReportTemplateFactory
    {
        internal static ISarReportTemplate MakeIControl(CommonParam param, object[] data)
        {
            ISarReportTemplate result = null;
            try
            {
                result = new SarReportTemplateBehavior(param, data);

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