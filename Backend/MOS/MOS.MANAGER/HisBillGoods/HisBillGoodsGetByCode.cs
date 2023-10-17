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
        internal HIS_BILL_GOODS GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBillGoodsFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BILL_GOODS GetByCode(string code, HisBillGoodsFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBillGoodsDAO.GetByCode(code, filter.Query());
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
