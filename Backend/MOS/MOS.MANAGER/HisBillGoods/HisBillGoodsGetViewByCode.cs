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
        internal V_HIS_BILL_GOODS GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBillGoodsViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BILL_GOODS GetViewByCode(string code, HisBillGoodsViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBillGoodsDAO.GetViewByCode(code, filter.Query());
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
