using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MetyMaty.Change
{
    public class MetyMatyADO : HIS_METY_MATY
    {
        public MetyMatyADO() { }

        public MetyMatyADO(HIS_METY_MATY MetyMaty, HIS_MEDICINE_TYPE medicineType)
        {
            this.ID = MetyMaty.ID;
            this.METY_PRODUCT_ID = MetyMaty.METY_PRODUCT_ID;
            this.MATERIAL_TYPE_ID = MetyMaty.MATERIAL_TYPE_ID;
            this.MATERIAL_TYPE_AMOUNT = MetyMaty.MATERIAL_TYPE_AMOUNT;
            if (medicineType != null)
            {
                this.MEDICINE_TYPE_CODE = medicineType.MEDICINE_TYPE_CODE;
                this.MEDICINE_TYPE_NAME = medicineType.MEDICINE_TYPE_NAME;
            }
        }

        public MetyMatyADO(HIS_METY_METY MetyMety, HIS_MEDICINE_TYPE medicineType)
        {
            this.ID = MetyMety.ID;
            this.METY_PRODUCT_ID = MetyMety.METY_PRODUCT_ID;
            this.MATERIAL_TYPE_ID = MetyMety.PREPARATION_MEDICINE_TYPE_ID;
            this.MATERIAL_TYPE_AMOUNT = MetyMety.PREPARATION_AMOUNT;
            if (medicineType != null)
            {
                this.MEDICINE_TYPE_CODE = medicineType.MEDICINE_TYPE_CODE;
                this.MEDICINE_TYPE_NAME = medicineType.MEDICINE_TYPE_NAME;
            }
        }

        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
    }
}
