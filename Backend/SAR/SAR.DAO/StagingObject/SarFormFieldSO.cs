using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarFormFieldSO : StagingObjectBase
    {
        public SarFormFieldSO()
        {
            listSarFormFieldExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM_FIELD, bool>>> listSarFormFieldExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM_FIELD, bool>>>();
    }
}
