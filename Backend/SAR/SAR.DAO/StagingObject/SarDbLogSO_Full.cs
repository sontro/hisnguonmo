using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarDbLogSO : StagingObjectBase
    {
        public SarDbLogSO()
        {
            listSarDbLogExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVSarDbLogExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_DB_LOG, bool>>> listSarDbLogExpression = new List<System.Linq.Expressions.Expression<Func<SAR_DB_LOG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_DB_LOG, bool>>> listVSarDbLogExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_DB_LOG, bool>>>();
    }
}
