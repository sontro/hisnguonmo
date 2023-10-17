using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisNoneMediServiceSO : StagingObjectBase
    {
        public HisNoneMediServiceSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_NONE_MEDI_SERVICE, bool>>> listHisNoneMediServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NONE_MEDI_SERVICE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_NONE_MEDI_SERVICE, bool>>> listVHisNoneMediServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_NONE_MEDI_SERVICE, bool>>>();
    }
}
