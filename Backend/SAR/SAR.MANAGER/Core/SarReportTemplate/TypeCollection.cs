using SAR.EFMODEL.DataModels;
using SAR.SDO;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class TypeCollection
    {
        internal static readonly List<Type> SarReportTemplate = new List<Type>() { typeof(SAR_REPORT_TEMPLATE), typeof(List<SAR_REPORT_TEMPLATE>), typeof(SarReportTemplateSDO), typeof(long) };
    }
}
