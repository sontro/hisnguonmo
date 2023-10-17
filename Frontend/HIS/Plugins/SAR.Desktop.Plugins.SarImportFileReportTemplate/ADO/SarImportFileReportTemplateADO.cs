using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAR.EFMODEL.DataModels;

namespace SAR.Desktop.Plugins.SarImportFileReportTemplate.ADO
{
    class SarImportFileReportTemplateADO:SAR_REPORT_TEMPLATE
    {
        public string ERROR { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

    }
}
