using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.ADO
{
    public class DMediStock1ADO : D_HIS_MEDI_STOCK_1
    {
        public string MEDICINE_TYPE_CODE__UNSIGN { get; set; }
        public string MEDICINE_TYPE_NAME__UNSIGN { get; set; }
        public string ACTIVE_INGR_BHYT_NAME__UNSIGN { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public long? TDL_MATERIAL_MAX_REUSE_COUNT { get; set; }//Số lần sử dụng tối đa
        public long? REMAIN_REUSE_COUNT { get; set; }//Số lần sử dụng còn lại
        public long? USE_COUNT { get; set; }//Số lần sử dụng
        public string USE_COUNT_DISPLAY { get; set; }//Số lần sử dụng
        public long MATERIAL_TYPE_ID { get; set; }
        public short? IS_KIDNEY { get; set; }
        public short? IS_FILM { get; set; }
        public string TDL_PACKAGE_NUMBER { get; set; }
        public string REGISTER_NUMBER { get; set; }
        public long? EXPIRED_DATE { get; set; }
        public bool? IsAssignPackage { get; set; }
        public long? MAME_ID { get; set; }
        public bool? IsUseOrginalUnitForPres { get; set; }
    }
}
