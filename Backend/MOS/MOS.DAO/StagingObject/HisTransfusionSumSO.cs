using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTransfusionSumSO : StagingObjectBase
    {
        public HisTransfusionSumSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION_SUM, bool>>> listHisTransfusionSumExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION_SUM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSFUSION_SUM, bool>>> listVHisTransfusionSumExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRANSFUSION_SUM, bool>>>();
    }
}
