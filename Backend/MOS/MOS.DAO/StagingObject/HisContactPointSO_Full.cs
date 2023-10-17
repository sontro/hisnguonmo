using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisContactPointSO : StagingObjectBase
    {
        public HisContactPointSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_CONTACT_POINT, bool>>> listHisContactPointExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONTACT_POINT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_CONTACT_POINT, bool>>> listVHisContactPointExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CONTACT_POINT, bool>>>();
    }
}
