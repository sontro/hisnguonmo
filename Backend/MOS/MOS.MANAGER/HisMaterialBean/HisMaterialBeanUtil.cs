using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialBean
{
    class HisMaterialBeanUtil
    {
        internal static void SetTdl(HIS_MATERIAL_BEAN bean, HIS_MATERIAL material)
        {
            if (bean != null && material != null)
            {
                bean.TDL_MATERIAL_EXPIRED_DATE = material.EXPIRED_DATE;
                bean.TDL_MATERIAL_IMP_PRICE = material.IMP_PRICE;
                bean.TDL_MATERIAL_IMP_TIME = material.IMP_TIME;
                bean.TDL_MATERIAL_IMP_VAT_RATIO = material.IMP_VAT_RATIO;
                bean.TDL_MATERIAL_IS_ACTIVE = material.IS_ACTIVE;
                bean.TDL_MATERIAL_TYPE_ID = material.MATERIAL_TYPE_ID;
                bean.TDL_IS_SALE_EQUAL_IMP_PRICE = material.IS_SALE_EQUAL_IMP_PRICE;
                bean.TDL_SERVICE_ID = material.TDL_SERVICE_ID;
                bean.TDL_MATERIAL_MAX_REUSE_COUNT = material.MAX_REUSE_COUNT;
                bean.TDL_PACKAGE_NUMBER = material.PACKAGE_NUMBER;
            }
        }

        internal static void SetTdl(HIS_MATERIAL_BEAN newBean, HIS_MATERIAL_BEAN oldBean)
        {
            if (newBean != null && oldBean != null)
            {
                newBean.TDL_MATERIAL_EXPIRED_DATE = oldBean.TDL_MATERIAL_EXPIRED_DATE;
                newBean.TDL_MATERIAL_IMP_PRICE = oldBean.TDL_MATERIAL_IMP_PRICE;
                newBean.TDL_MATERIAL_IMP_TIME = oldBean.TDL_MATERIAL_IMP_TIME;
                newBean.TDL_MATERIAL_IMP_VAT_RATIO = oldBean.TDL_MATERIAL_IMP_VAT_RATIO;
                newBean.TDL_MATERIAL_IS_ACTIVE = oldBean.TDL_MATERIAL_IS_ACTIVE;
                newBean.TDL_MATERIAL_TYPE_ID = oldBean.TDL_MATERIAL_TYPE_ID;
                newBean.TDL_IS_SALE_EQUAL_IMP_PRICE = oldBean.TDL_IS_SALE_EQUAL_IMP_PRICE;
                newBean.TDL_SERVICE_ID = oldBean.TDL_SERVICE_ID;
                newBean.TDL_MATERIAL_MAX_REUSE_COUNT = oldBean.TDL_MATERIAL_MAX_REUSE_COUNT;
                newBean.TDL_PACKAGE_NUMBER = oldBean.TDL_PACKAGE_NUMBER;
            }
        }
    }
}
