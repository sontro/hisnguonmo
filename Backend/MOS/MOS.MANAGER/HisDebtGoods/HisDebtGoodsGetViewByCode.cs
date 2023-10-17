using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebtGoods
{
    partial class HisDebtGoodsGet : BusinessBase
    {
        internal V_HIS_DEBT_GOODS GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDebtGoodsViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEBT_GOODS GetViewByCode(string code, HisDebtGoodsViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebtGoodsDAO.GetViewByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
