using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportTest.ADO
{
    class DataReportADO
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string ValueRowCell { get; set; }
        public string Font { get; set; }
        public string Color { get; set; }
        public string SheetName { get; set; }
        public string REPORT_CODE { get; set; }
        public int Status { get; set; }
        //1 Đang tao bao cao nguon
        //2 Đang tao bao cao dich
        //3 Hoan thanh
        public string REPORT_TYPE_CODE { get; set; }
        public bool Result { get; set; }

    }
}
