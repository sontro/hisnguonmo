using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarUserReportTypeSO : StagingObjectBase
    {
        public SarUserReportTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_USER_REPORT_TYPE, bool>>> listSarUserReportTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_USER_REPORT_TYPE, bool>>>();
    }
}
