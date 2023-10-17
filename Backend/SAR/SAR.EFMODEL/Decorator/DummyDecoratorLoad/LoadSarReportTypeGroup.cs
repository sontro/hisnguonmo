using SAR.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace SAR.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadSarReportTypeGroup()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(SAR_REPORT_TYPE_GROUP).GetProperty("REPORT_TYPE_GROUP_CODE"));
            properties[typeof(SAR_REPORT_TYPE_GROUP)] = pies;
        }
    }
}
