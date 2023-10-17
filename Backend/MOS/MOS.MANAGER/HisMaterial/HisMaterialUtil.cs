using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialUtil
    {
        public static void SetTdl(HIS_MATERIAL material, HIS_MATERIAL_TYPE materialType)
        {
            if (material != null && materialType != null)
            {
                material.TDL_SERVICE_ID = materialType.SERVICE_ID;
                material.TDL_IMP_UNIT_CONVERT_RATIO = materialType.IMP_UNIT_CONVERT_RATIO;
                material.TDL_IMP_UNIT_ID = materialType.IMP_UNIT_ID;
            }
        }

        public static void SetTdl(HIS_MATERIAL material, HIS_IMP_MEST impMest)
        {
            if (material != null && impMest != null)
            {
                material.TDL_IMP_MEST_CODE = impMest.IMP_MEST_CODE;
                material.TDL_IMP_MEST_SUB_CODE = impMest.IMP_MEST_SUB_CODE;
            }
        }

        public static bool CheckIsDiff(HIS_MATERIAL newMaterial, HIS_MATERIAL oldMaterial)
        {
            if (newMaterial != null && oldMaterial != null)
            {
                return (
                    newMaterial.AMOUNT != oldMaterial.AMOUNT
                    || newMaterial.BID_ID != oldMaterial.BID_ID
                    || newMaterial.EXPIRED_DATE != oldMaterial.EXPIRED_DATE
                    || newMaterial.IMP_PRICE != oldMaterial.IMP_PRICE
                    || newMaterial.IMP_SOURCE_ID != oldMaterial.IMP_SOURCE_ID
                    || newMaterial.IMP_VAT_RATIO != oldMaterial.IMP_VAT_RATIO
                    || newMaterial.INTERNAL_PRICE != oldMaterial.INTERNAL_PRICE
                    || newMaterial.IS_PREGNANT != oldMaterial.IS_PREGNANT
                    || newMaterial.IS_SALE_EQUAL_IMP_PRICE != oldMaterial.IS_SALE_EQUAL_IMP_PRICE
                    || newMaterial.MATERIAL_TYPE_ID != oldMaterial.MATERIAL_TYPE_ID
                    || newMaterial.PACKAGE_NUMBER != oldMaterial.PACKAGE_NUMBER
                    || newMaterial.SUPPLIER_ID != oldMaterial.SUPPLIER_ID
                    || newMaterial.CONCENTRA != oldMaterial.CONCENTRA
                    || newMaterial.MANUFACTURER_ID != oldMaterial.MANUFACTURER_ID
                    || newMaterial.NATIONAL_NAME != oldMaterial.NATIONAL_NAME
                    || newMaterial.TDL_BID_GROUP_CODE != oldMaterial.TDL_BID_GROUP_CODE
                    || newMaterial.TDL_BID_NUM_ORDER != oldMaterial.TDL_BID_NUM_ORDER
                    || newMaterial.TDL_BID_NUMBER != oldMaterial.TDL_BID_NUMBER
                    || newMaterial.TDL_BID_PACKAGE_CODE != oldMaterial.TDL_BID_PACKAGE_CODE
                    || newMaterial.TDL_BID_YEAR != oldMaterial.TDL_BID_YEAR
                    || newMaterial.TDL_SERVICE_ID != oldMaterial.TDL_SERVICE_ID
                    || newMaterial.TDL_IMP_UNIT_ID != oldMaterial.TDL_IMP_UNIT_ID
                    || newMaterial.IMP_UNIT_AMOUNT != oldMaterial.IMP_UNIT_AMOUNT
                    || newMaterial.IMP_UNIT_PRICE != oldMaterial.IMP_UNIT_PRICE
                    || newMaterial.TDL_IMP_UNIT_CONVERT_RATIO != oldMaterial.TDL_IMP_UNIT_CONVERT_RATIO
                    );
            }
            else
            {
                return true;
            }
        }
    }
}
