using SAR.EFMODEL.DataModels;
using System.Web;

namespace SAR.SDO
{
    public class SarReportTemplateSDO : SAR_REPORT_TEMPLATE
    {
        public HttpFileCollectionBase FileUpload { get; set; }

        public SarReportTemplateSDO()
            : base()
        {

        }
    }
}
