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
        internal List<V_HIS_DEBT_GOODS> GetView(HisDebtGoodsViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebtGoodsDAO.GetView(filter.Query(), param);
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
