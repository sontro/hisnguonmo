using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillGoods
{
    partial class HisBillGoodsGet : BusinessBase
    {
        internal List<V_HIS_BILL_GOODS> GetView(HisBillGoodsViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBillGoodsDAO.GetView(filter.Query(), param);
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
