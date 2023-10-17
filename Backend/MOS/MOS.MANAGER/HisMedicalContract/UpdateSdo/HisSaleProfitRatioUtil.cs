using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.UpdateSdo
{
    class HisSaleProfitRatioUtil
    {
        public static decimal GetProfitRatio(decimal price, long serviceUnitId, List<HIS_SALE_PROFIT_CFG> saleProfitCfgs, bool isMedicine)
        {
            try
            {
                HIS_SERVICE_UNIT serviceUnit = HisServiceUnitCFG.DATA.Where(o => o.ID == serviceUnitId).FirstOrDefault();
                
                if (serviceUnit != null && serviceUnit.IS_PRIMARY == 1)
                {
                    HIS_SALE_PROFIT_CFG profit = saleProfitCfgs.Where(p => (isMedicine ? p.IS_MEDICINE == Constant.IS_TRUE : p.IS_MATERIAL == Constant.IS_TRUE)
                        && ((p.IMP_PRICE_FROM != null && p.IMP_PRICE_TO != null) ? (p.IMP_PRICE_FROM <= price && p.IMP_PRICE_TO >= price) :
                            ((p.IMP_PRICE_FROM != null && p.IMP_PRICE_TO == null) ? p.IMP_PRICE_FROM <= price :
                                ((p.IMP_PRICE_FROM == null && p.IMP_PRICE_TO != null) ? p.IMP_PRICE_TO >= price : true)))
                        ).OrderByDescending(p => p.MODIFY_TIME).FirstOrDefault();
                    return profit != null ? profit.RATIO/100 : 0; //can chia cho 100 do chuc nang nay khi luu vao DB chua chia cho 100
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return 0;
        }
    }
}
