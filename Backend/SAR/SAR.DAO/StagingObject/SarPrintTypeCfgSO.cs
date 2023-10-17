using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarPrintTypeCfgSO : StagingObjectBase
    {
        public SarPrintTypeCfgSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE_CFG, bool>>> listSarPrintTypeCfgExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE_CFG, bool>>>();
    }
}
