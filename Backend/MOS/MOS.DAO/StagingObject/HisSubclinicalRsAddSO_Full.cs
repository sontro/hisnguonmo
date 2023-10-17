using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSubclinicalRsAddSO : StagingObjectBase
    {
        public HisSubclinicalRsAddSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SUBCLINICAL_RS_ADD, bool>>> listHisSubclinicalRsAddExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SUBCLINICAL_RS_ADD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SUBCLINICAL_RS_ADD, bool>>> listVHisSubclinicalRsAddExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SUBCLINICAL_RS_ADD, bool>>>();
    }
}
