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
        internal HisDebtGoodsGet()
            : base()
        {

        }

        internal HisDebtGoodsGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEBT_GOODS> Get(HisDebtGoodsFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebtGoodsDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBT_GOODS GetById(long id)
        {
            try
            {
                return GetById(id, new HisDebtGoodsFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBT_GOODS GetById(long id, HisDebtGoodsFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebtGoodsDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_DEBT_GOODS> GetByDebtIds(List<long> debtIds)
        {
            HisDebtGoodsFilterQuery filter = new HisDebtGoodsFilterQuery();
            filter.DEBT_IDs = debtIds;
            return this.Get(filter);
        }
    }
}
