using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarRetyFofiSO : StagingObjectBase
    {
        public SarRetyFofiSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_RETY_FOFI, bool>>> listSarRetyFofiExpression = new List<System.Linq.Expressions.Expression<Func<SAR_RETY_FOFI, bool>>>();
    }
}
