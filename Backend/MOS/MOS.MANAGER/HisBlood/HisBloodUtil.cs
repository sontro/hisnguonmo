using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBlood
{
    class HisBloodUtil
    {
        internal static bool CheckIsDiff(HIS_BLOOD newBlood, HIS_BLOOD oldBlood)
        {
            if (newBlood != null && oldBlood != null)
            {
                return (
                    newBlood.BID_ID != oldBlood.BID_ID
                    || newBlood.BID_NUM_ORDER != oldBlood.BID_NUM_ORDER
                    || newBlood.BLOOD_ABO_ID != oldBlood.BLOOD_ABO_ID
                    || newBlood.BLOOD_CODE != oldBlood.BLOOD_CODE
                    || newBlood.BLOOD_RH_ID != oldBlood.BLOOD_RH_ID
                    || newBlood.BLOOD_TYPE_ID != oldBlood.BLOOD_TYPE_ID
                    || newBlood.EXPIRED_DATE != oldBlood.EXPIRED_DATE
                    || newBlood.GIVE_CODE != oldBlood.GIVE_CODE
                    || newBlood.GIVE_NAME != oldBlood.GIVE_NAME
                    || newBlood.IMP_PRICE != oldBlood.IMP_PRICE
                    || newBlood.IMP_SOURCE_ID != oldBlood.IMP_SOURCE_ID
                    || newBlood.IMP_VAT_RATIO != oldBlood.IMP_VAT_RATIO
                    || newBlood.INTERNAL_PRICE != oldBlood.INTERNAL_PRICE
                    || newBlood.IS_PREGNANT != oldBlood.IS_PREGNANT
                    || newBlood.MEDI_STOCK_ID != oldBlood.MEDI_STOCK_ID
                    || newBlood.PACKAGE_NUMBER != oldBlood.PACKAGE_NUMBER
                    || newBlood.PACKING_TIME != oldBlood.PACKING_TIME
                    || newBlood.SUPPLIER_ID != oldBlood.SUPPLIER_ID
                    );
            }
            else
            {
                return true;
            }
        }
    }
}
