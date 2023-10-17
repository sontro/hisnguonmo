using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    public class MediMateCheckADO : V_HIS_MEDICINE_TYPE
    {
        public long? MATERIAL_TYPE_MAP_ID { get; set; }
        public string MATERIAL_TYPE_MAP_CODE { get; set; }
        public string MATERIAL_TYPE_MAP_NAME { get; set; }
        public decimal? AMOUNT { get; set; }
        public long? MAX_REUSE_COUNT { get; set; }//Số lần sử dụng tối đa
        public long? USE_COUNT { get; set; }//Số lần sử dụng
        public long? USE_REMAIN_COUNT { get; set; }//Số lần sử dụng còn lại
        public string Sang { get; set; }
        public string Trua { get; set; }
        public string Chieu { get; set; }
        public string Toi { get; set; }
        public long? UseTimeTo { get; set; }
        public decimal? UseDays { get; set; }

        public MediMateCheckADO()
        {

        }
    }
}
