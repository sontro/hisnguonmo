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
        internal HIS_DEBT_GOODS GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDebtGoodsFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBT_GOODS GetByCode(string code, HisDebtGoodsFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebtGoodsDAO.GetByCode(code, filter.Query());
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
