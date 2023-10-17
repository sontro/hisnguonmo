using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarPrintSO : StagingObjectBase
    {
        public SarPrintSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT, bool>>> listSarPrintExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT, bool>>> listVSarPrintExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_PRINT, bool>>>();
    }
}
