using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarPrintLogSO : StagingObjectBase
    {
        public SarPrintLogSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT_LOG, bool>>> listSarPrintLogExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_LOG, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT_LOG, bool>>> listVSarPrintLogExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT_LOG, bool>>>();
    }
}
