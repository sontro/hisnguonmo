using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSaleProfitCfgSO : StagingObjectBase
    {
        public HisSaleProfitCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SALE_PROFIT_CFG, bool>>> listHisSaleProfitCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SALE_PROFIT_CFG, bool>>>();
    }
}
