using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMetyProductSO : StagingObjectBase
    {
        public HisMetyProductSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_METY_PRODUCT, bool>>> listHisMetyProductExpression = new List<System.Linq.Expressions.Expression<Func<HIS_METY_PRODUCT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_METY_PRODUCT, bool>>> listVHisMetyProductExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_METY_PRODUCT, bool>>>();
    }
}
