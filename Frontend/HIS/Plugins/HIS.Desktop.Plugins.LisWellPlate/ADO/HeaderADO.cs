using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.LisWellPlate.ADO
{
    class HeaderADO
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public string BARCODE { get; set; }
        public long? SERVICE_RESULT_ID { get; set; }
        public long? SAMPLE_ID { get; set; }
        public string LAST_NAME {get;set;}
        public string FIRST_NAME {get;set;}
        public string GENDER_NAME {get;set;}
        public string DOB {get;set;}
        public long DETAIL_ID { get; set; }
        public HeaderADO()
        {

        }

    }
}
