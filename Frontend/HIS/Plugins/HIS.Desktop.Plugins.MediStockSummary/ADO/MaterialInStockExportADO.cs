using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.ADO
{
    public class MaterialInStockExportADO : MOS.EFMODEL.DataModels.V_HIS_MATERIAL_BEAN
    {
        public decimal? AVAILABLE_AMOUNT { get; set; }
        public string IS_CHEMICAL_SUBSTANCE_STR { get; set; }//la hoa chat
        public string EXPIRED_DATE_STR { get; set; }
        public string ALERT_EXPIRED_DATE_STR { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }//Đối tuowjgn thanh toán
        public decimal IMP_PRICE_VAT { get; set; } //gia nhap sau VAT
        public decimal EXP_PRICE { get; set; }
        public decimal EXP_VAT_RATIO { get; set; }
        public string PATIENT_TYPE_NAME_1 { get; set; }
        public string PATIENT_TYPE_NAME_2 { get; set; }
        public string PATIENT_TYPE_NAME_3 { get; set; }
        public string PATIENT_TYPE_NAME_4 { get; set; }
        public string PATIENT_TYPE_NAME_5 { get; set; }
        public string PATIENT_TYPE_NAME_6 { get; set; }
        public string PATIENT_TYPE_NAME_7 { get; set; }
        public string PATIENT_TYPE_NAME_8 { get; set; }
        public string PATIENT_TYPE_NAME_9 { get; set; }
        public string PATIENT_TYPE_NAME_10 { get; set; }
        public decimal EXP_PRICE_1 { get; set; }
        public decimal EXP_PRICE_2 { get; set; }
        public decimal EXP_PRICE_3 { get; set; }
        public decimal EXP_PRICE_4 { get; set; }
        public decimal EXP_PRICE_5 { get; set; }
        public decimal EXP_PRICE_6 { get; set; }
        public decimal EXP_PRICE_7 { get; set; }
        public decimal EXP_PRICE_8 { get; set; }
        public decimal EXP_PRICE_9 { get; set; }
        public decimal EXP_PRICE_10 { get; set; }

        public Dictionary<string, string> DicPatientTypeName { get; set; }
        public Dictionary<string, decimal> DicExpPrice { get; set; }
        public Dictionary<string, decimal> DicExpVatRatio { get; set; }
    }
}
