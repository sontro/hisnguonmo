using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgGet : BusinessBase
    {
        internal HisSaleProfitCfgGet()
            : base()
        {

        }

        internal HisSaleProfitCfgGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SALE_PROFIT_CFG> Get(HisSaleProfitCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSaleProfitCfgDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SALE_PROFIT_CFG GetById(long id)
        {
            try
            {
                return GetById(id, new HisSaleProfitCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SALE_PROFIT_CFG GetById(long id, HisSaleProfitCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSaleProfitCfgDAO.GetById(id, filter.Query());
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
