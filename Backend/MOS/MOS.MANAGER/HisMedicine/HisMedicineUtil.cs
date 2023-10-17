using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineUtil
    {
        public static void SetTdl(HIS_MEDICINE medicine, HIS_MEDICINE_TYPE medicineType)
        {
            if (medicine != null && medicineType != null)
            {
                medicine.TDL_SERVICE_ID = medicineType.SERVICE_ID;
                medicine.TDL_IMP_UNIT_ID = medicineType.IMP_UNIT_ID;
                medicine.TDL_IMP_UNIT_CONVERT_RATIO = medicineType.IMP_UNIT_CONVERT_RATIO;
            }
        }

        public static void SetTdl(HIS_MEDICINE medicine, HIS_IMP_MEST impMest)
        {
            if (medicine != null && impMest != null)
            {
                medicine.TDL_IMP_MEST_CODE = impMest.IMP_MEST_CODE;
                medicine.TDL_IMP_MEST_SUB_CODE = impMest.IMP_MEST_SUB_CODE;
            }
        }

        public static bool CheckIsDiff(HIS_MEDICINE newMedicine, HIS_MEDICINE oldMedicine)
        {
            if (newMedicine != null && oldMedicine != null)
            {
                return (
                    newMedicine.AMOUNT != oldMedicine.AMOUNT
                    || newMedicine.BID_ID != oldMedicine.BID_ID
                    || newMedicine.EXPIRED_DATE != oldMedicine.EXPIRED_DATE
                    || newMedicine.IMP_PRICE != oldMedicine.IMP_PRICE
                    || newMedicine.IMP_SOURCE_ID != oldMedicine.IMP_SOURCE_ID
                    || newMedicine.IMP_VAT_RATIO != oldMedicine.IMP_VAT_RATIO
                    || newMedicine.INTERNAL_PRICE != oldMedicine.INTERNAL_PRICE
                    || newMedicine.IS_PREGNANT != oldMedicine.IS_PREGNANT
                    || newMedicine.IS_SALE_EQUAL_IMP_PRICE != oldMedicine.IS_SALE_EQUAL_IMP_PRICE
                    || newMedicine.MEDICINE_BYT_NUM_ORDER != oldMedicine.MEDICINE_BYT_NUM_ORDER
                    || newMedicine.MEDICINE_IS_STAR_MARK != oldMedicine.MEDICINE_IS_STAR_MARK
                    || newMedicine.MEDICINE_REGISTER_NUMBER != oldMedicine.MEDICINE_REGISTER_NUMBER
                    || newMedicine.MEDICINE_TCY_NUM_ORDER != oldMedicine.MEDICINE_TCY_NUM_ORDER
                    || newMedicine.MEDICINE_TYPE_ID != oldMedicine.MEDICINE_TYPE_ID
                    || newMedicine.PACKAGE_NUMBER != oldMedicine.PACKAGE_NUMBER
                    || newMedicine.SUPPLIER_ID != oldMedicine.SUPPLIER_ID
                    || newMedicine.MANUFACTURER_ID != oldMedicine.MANUFACTURER_ID
                    || newMedicine.CONCENTRA != oldMedicine.CONCENTRA
                    || newMedicine.ACTIVE_INGR_BHYT_CODE != oldMedicine.ACTIVE_INGR_BHYT_CODE
                    || newMedicine.ACTIVE_INGR_BHYT_NAME != oldMedicine.ACTIVE_INGR_BHYT_NAME
                    || newMedicine.TDL_BID_GROUP_CODE != oldMedicine.TDL_BID_GROUP_CODE
                    || newMedicine.TDL_BID_NUM_ORDER != oldMedicine.TDL_BID_NUM_ORDER
                    || newMedicine.TDL_BID_NUMBER != oldMedicine.TDL_BID_NUMBER
                    || newMedicine.TDL_BID_PACKAGE_CODE != oldMedicine.TDL_BID_PACKAGE_CODE
                    || newMedicine.TDL_BID_YEAR != oldMedicine.TDL_BID_YEAR
                    || newMedicine.TDL_SERVICE_ID != oldMedicine.TDL_SERVICE_ID
                    || newMedicine.TDL_IMP_UNIT_ID != oldMedicine.TDL_IMP_UNIT_ID
                    || newMedicine.IMP_UNIT_AMOUNT != oldMedicine.IMP_UNIT_AMOUNT
                    || newMedicine.IMP_UNIT_PRICE != oldMedicine.IMP_UNIT_PRICE
                    || newMedicine.TDL_IMP_UNIT_CONVERT_RATIO != oldMedicine.TDL_IMP_UNIT_CONVERT_RATIO
                    );
            }
            else
            {
                return true;
            }
        }
    }
}
