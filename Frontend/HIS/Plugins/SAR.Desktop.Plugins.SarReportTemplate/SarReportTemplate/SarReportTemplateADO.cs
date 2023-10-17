using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.Desktop.Plugins.SarReportTemplate.SarReportTemplate
{
    class SarReportTemplateADO : SAR_REPORT_TEMPLATE
    {
        
    public  SarReportTemplateADO (SAR_REPORT_TEMPLATE data)
    {
         Inventec.Common.Mapper.DataObjectMapper.Map<SarReportTemplateADO>(this, data);
    }
    }
}
