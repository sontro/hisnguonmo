using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SimpleEventKey : Attribute
    {
        public const string PATIENT_CODE = "PATIENT_CODE";
        public const string TREATMENT_CODE = "TREATMENT_CODE";
        public const string SERVICE_REQ_CODE = "SERVICE_REQ_CODE";
        public const string VACCINATION_CODE = "VACCINATION_CODE";
        public const string EXP_MEST_CODE = "EXP_MEST_CODE";
        public const string IMP_MEST_CODE = "IMP_MEST_CODE";
        public const string DISPENSE_CODE = "DISPENSE_CODE";
        public const string TRANSACTION_CODE = "TRANSACTION_CODE";
        public const string INVOICE_NUM_ORDER = "INVOICE_NUM_ORDER";
        public const string AGGR_EXP_MEST_CODE = "AGGR_EXP_MEST_CODE";
        public const string AGGR_IMP_MEST_CODE = "AGGR_IMP_MEST_CODE";
        public const string PREPARE_CODE = "PREPARE_CODE";
        public const string BID_ID = "BID_ID";
        public const string MEDICINE_TYPE_ID = "MEDICINE_TYPE_ID";
        public const string MATERIAL_TYPE_ID = "MATERIAL_TYPE_ID";
        public const string BLOOD_TYPE_ID = "BLOOD_TYPE_ID";
        public const string MEDICINE_TYPE_CODE = "MEDICINE_TYPE_CODE";
        public const string MATERIAL_TYPE_CODE = "MATERIAL_TYPE_CODE";
        public const string BLOOD_TYPE_CODE = "BLOOD_TYPE_CODE";
        public const string DETAIL = "DETAIL";
        public const string BID_NUMBER = "BID_NUMBER";
        public const string ANTIBIOTIC_REQUEST_CODE = "ANTIBIOTIC_REQUEST_CODE";
        public const string STORE_CODE = "STORE_CODE";
        public const string MEDICINE_ID = "MEDICINE_ID";
        public const string SERVICE_CODE = "SERVICE_CODE";
        public const string MATERIAL_ID = "MATERIAL_ID";
        public const string KEY = "KEY";

        public string Key { get; set; } //gia tri khai bao trong message mẫu
        public string Value { get; set; } //gia tri se duoc replace vào "key" de ghi vao log

        public SimpleEventKey(string key, string value)
        {
            this.Value = value;
            this.Key = key;
        }

        public SimpleEventKey(string key)
        {
            this.Value = key;//mac dinh key va value giong nhau
            this.Key = key;
        }
    }
}
