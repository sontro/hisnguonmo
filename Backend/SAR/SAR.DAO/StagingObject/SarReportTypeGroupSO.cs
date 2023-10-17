using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarReportTypeGroupSO : StagingObjectBase
    {
        public SarReportTypeGroupSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_REPORT_TYPE_GROUP, bool>>> listSarReportTypeGroupExpression = new List<System.Linq.Expressions.Expression<Func<SAR_REPORT_TYPE_GROUP, bool>>>();
    }
}
