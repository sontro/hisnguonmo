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
            listSarRetyFofiExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
            listVSarRetyFofiExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_RETY_FOFI, bool>>> listSarRetyFofiExpression = new List<System.Linq.Expressions.Expression<Func<SAR_RETY_FOFI, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_RETY_FOFI, bool>>> listVSarRetyFofiExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_RETY_FOFI, bool>>>();
    }
}
