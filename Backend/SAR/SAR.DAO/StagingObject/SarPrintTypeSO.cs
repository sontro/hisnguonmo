using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarPrintTypeSO : StagingObjectBase
    {
        public SarPrintTypeSO()
        {
            listSarPrintTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE, bool>>> listSarPrintTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE, bool>>>();
    }
}
