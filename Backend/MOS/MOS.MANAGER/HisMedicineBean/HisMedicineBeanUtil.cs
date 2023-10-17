using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineBean
{
    class HisMedicineBeanUtil
    {
        internal static void SetTdl(HIS_MEDICINE_BEAN bean, HIS_MEDICINE medicine)
        {
            if (bean != null && medicine != null)
            {
                bean.TDL_MEDICINE_EXPIRED_DATE = medicine.EXPIRED_DATE;
                bean.TDL_MEDICINE_IMP_PRICE = medicine.IMP_PRICE;
                bean.TDL_MEDICINE_IMP_TIME = medicine.IMP_TIME;
                bean.TDL_MEDICINE_IMP_VAT_RATIO = medicine.IMP_VAT_RATIO;
                bean.TDL_MEDICINE_IS_ACTIVE = medicine.IS_ACTIVE;
                bean.TDL_MEDICINE_TYPE_ID = medicine.MEDICINE_TYPE_ID;
                bean.TDL_IS_SALE_EQUAL_IMP_PRICE = medicine.IS_SALE_EQUAL_IMP_PRICE;
                bean.TDL_SERVICE_ID = medicine.TDL_SERVICE_ID;
                bean.TDL_PACKAGE_NUMBER = medicine.PACKAGE_NUMBER;
                bean.TDL_MEDICINE_REGISTER_NUMBER = medicine.MEDICINE_REGISTER_NUMBER;
            }
        }

        internal static void SetTdl(HIS_MEDICINE_BEAN newBean, HIS_MEDICINE_BEAN oldBean)
        {
            if (newBean != null && oldBean != null)
            {
                newBean.TDL_MEDICINE_EXPIRED_DATE = oldBean.TDL_MEDICINE_EXPIRED_DATE;
                newBean.TDL_MEDICINE_IMP_PRICE = oldBean.TDL_MEDICINE_IMP_PRICE;
                newBean.TDL_MEDICINE_IMP_TIME = oldBean.TDL_MEDICINE_IMP_TIME;
                newBean.TDL_MEDICINE_IMP_VAT_RATIO = oldBean.TDL_MEDICINE_IMP_VAT_RATIO;
                newBean.TDL_MEDICINE_IS_ACTIVE = oldBean.TDL_MEDICINE_IS_ACTIVE;
                newBean.TDL_MEDICINE_TYPE_ID = oldBean.TDL_MEDICINE_TYPE_ID;
                newBean.TDL_IS_SALE_EQUAL_IMP_PRICE = oldBean.TDL_IS_SALE_EQUAL_IMP_PRICE;
                newBean.TDL_SERVICE_ID = oldBean.TDL_SERVICE_ID;
                newBean.TDL_PACKAGE_NUMBER = newBean.TDL_PACKAGE_NUMBER;
                newBean.TDL_MEDICINE_REGISTER_NUMBER = newBean.TDL_MEDICINE_REGISTER_NUMBER;
            }
        }
    }
}
