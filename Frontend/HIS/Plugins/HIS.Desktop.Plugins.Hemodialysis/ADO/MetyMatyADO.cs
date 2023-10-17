using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Hemodialysis.ADO
{
    public class MetyMatyADO
    {
        public HIS_SERVICE_REQ_METY ReqMety { get; set; }
        public HIS_EXP_MEST_MEDICINE ExpMestMedicine { get; set; }

        public string MedicineTypeCode { get; set; }
        public string MedicineTypeName { get; set; }
        public string ServiceUnitName { get; set; }
        public long ServiceReqId { get; set; }
        public long MedicineTypeId { get; set; }
        public decimal Amount { get; set; }
        public bool IsRequest { get; set; }
        public bool IsKidney { get; set; }

        public MetyMatyADO() { }

        public MetyMatyADO(HIS_EXP_MEST_MEDICINE medicine, V_HIS_MEDICINE_TYPE medicineType)
        {
            if (medicine != null)
            {
                this.ServiceReqId = medicine.TDL_SERVICE_REQ_ID ?? 0;
                this.Amount = medicine.AMOUNT;
                this.IsRequest = false;
                this.MedicineTypeId = medicine.TDL_MEDICINE_TYPE_ID ?? 0;
                this.ExpMestMedicine = medicine;
            }
            if (medicineType != null)
            {
                this.MedicineTypeCode = medicineType.MEDICINE_TYPE_CODE;
                this.MedicineTypeId = medicineType.ID;
                this.MedicineTypeName = medicineType.MEDICINE_TYPE_NAME;
                this.ServiceUnitName = medicineType.SERVICE_UNIT_NAME;
                this.IsKidney = medicineType.IS_KIDNEY == 1;
            }
        }

        public MetyMatyADO(HIS_SERVICE_REQ_METY reqMety, V_HIS_MEDICINE_TYPE medicineType)
        {
            if (reqMety != null)
            {
                this.ServiceReqId = reqMety.SERVICE_REQ_ID;
                this.Amount = reqMety.AMOUNT;
                this.IsRequest = true;
                this.MedicineTypeId = reqMety.MEDICINE_TYPE_ID ?? 0;
                this.ReqMety = reqMety;
            }
            if (medicineType != null)
            {
                this.MedicineTypeCode = medicineType.MEDICINE_TYPE_CODE;
                this.MedicineTypeId = medicineType.ID;
                this.MedicineTypeName = medicineType.MEDICINE_TYPE_NAME;
                this.ServiceUnitName = medicineType.SERVICE_UNIT_NAME;
                this.IsKidney = medicineType.IS_KIDNEY == 1;
            }
        }
    }
}
