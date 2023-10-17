using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarPrintTemplateSO : StagingObjectBase
    {
        public SarPrintTemplateSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TEMPLATE, bool>>> listSarPrintTemplateExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TEMPLATE, bool>>>();
    }
}
