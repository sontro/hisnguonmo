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
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE, bool>>> listSarPrintTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT_TYPE, bool>>> listVSarPrintTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT_TYPE, bool>>>();
    }
}
