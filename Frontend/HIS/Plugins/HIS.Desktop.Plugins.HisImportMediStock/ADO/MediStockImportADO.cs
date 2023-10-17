using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportMediStock.ADO
{
    public class MediStockImportADO : V_HIS_MEDI_STOCK
    {
        public string AUTO_APPROVE_EXPORT { get; set; }
        public string AUTO_EXECUTE_EXPORT { get; set; }
        public string AUTO_APPROVE_IMPORT { get; set; }
        public string AUTO_EXECUTE_IMPORT { get; set; }

        public string IS_ALLOW_IMP_SUPPLIER_STR { get; set; }
        public string IS_AUTO_CREATE_CHMS_IMP_STR { get; set; }
        public string IS_BLOOD_STR { get; set; }
        public string IS_NEW_MEDICINE_STR { get; set; }
        public string IS_TRADITIONAL_MEDICINE_STR { get; set; }
        public string IS_BUSINESS_STR { get; set; }
        public string IS_CABINET_STR { get; set; }
        public string IS_GOODS_RESTRICT_STR { get; set; }
        public string IS_ODD_STR { get; set; }
        public string IS_SHOW_DDT_STR { get; set; }

        public bool ALLOW_IMP_SUPPLIER { get; set; }
        public bool AUTO_CREATE_CHMS_IMP { get; set; }
        public bool BLOOD { get; set; }
        public bool NEW_MEDICINE { get; set; }
        public bool TRADITIONAL_MEDICINE { get; set; }
        public bool CABINET { get; set; }
        public bool GOODS_RESTRICT { get; set; }
        public bool BUSINESS { get; set; }
        public bool ODD { get; set; }
        public bool SHOW_DDT { get; set; }

        public Dictionary<long, HIS_MEDI_STOCK_EXTY> dicMediStockExty { get; set; }
        public Dictionary<long, HIS_MEDI_STOCK_IMTY> dicMediStockImty { get; set; }

        public string ERROR { get; set; }

        public MediStockImportADO()
        {
        }

        public MediStockImportADO(V_HIS_MEDI_STOCK data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<MediStockImportADO>(this, data);
        }
    }
}
