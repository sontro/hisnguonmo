using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.SDO
{
    public class SarFormCreateOrUpdateSDO
    {
        public long? FormId { get; set; }
        public string FormTypeCode { get; set; }
        public string Description { get; set; }
        public List<SAR_FORM_DATA> FormData { get; set; }
    }
}
