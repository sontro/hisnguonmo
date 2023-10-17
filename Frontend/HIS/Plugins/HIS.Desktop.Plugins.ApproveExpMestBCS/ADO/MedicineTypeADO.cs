using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveExpMestBCS.ADO
{
    public class MedicineTypeADO
    {
        public long MEDICINE_TYPE_ID { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public string ACTIVE_INGR_BHYT_CODE { get; set; }
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string REPLACE_MEDICINE_TYPE_NAME { get; set; }
        public long? REPLACE_MEDICINE_TYPE_ID { get; set; }
        public string CONCENTRA { get; set; }

        public decimal AMOUNT { get; set; }
        public decimal DD_AMOUNT { get; set; }
        public decimal YCD_AMOUNT { get; set; }
        public bool IS_ALLOW_EXPORT_ODD { get; set; }
        public decimal CURRENT_DD_AMOUNT { get; set; }
        public decimal CURRENT_YC_AMOUNT { get; set; }
        public decimal TT_AMOUNT { get; set; }
        public decimal TON_KHO { get; set; }
        public decimal AVAIL_AMOUNT { get; set; }

        public bool IsReplace { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCheck { get; set; }

        public List<HIS_EXP_MEST_METY_REQ> Requests { get; set; }

        public MedicineTypeADO()
        {

        }

        public MedicineTypeADO(List<HIS_EXP_MEST_METY_REQ> medicines)
        {
            var first = medicines.FirstOrDefault();
            this.Requests = medicines;
            this.MEDICINE_TYPE_ID = first.MEDICINE_TYPE_ID;
            this.AMOUNT = medicines.Sum(s => s.AMOUNT);
            this.DD_AMOUNT = medicines.Sum(s => s.DD_AMOUNT ?? 0);
            this.CURRENT_DD_AMOUNT = this.DD_AMOUNT;
        }

        public MedicineTypeADO(List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            var first = medicines.FirstOrDefault();
            this.MEDICINE_TYPE_ID = first.TDL_MEDICINE_TYPE_ID ?? 0;
            this.AMOUNT =0;
            this.DD_AMOUNT = medicines.Sum(s => s.AMOUNT);
        }
    }
}
