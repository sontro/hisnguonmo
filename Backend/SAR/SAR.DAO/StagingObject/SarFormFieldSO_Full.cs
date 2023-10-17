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
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM_FIELD, bool>>> listSarFormFieldExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM_FIELD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_FIELD, bool>>> listVSarFormFieldExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_FIELD, bool>>>();
    }
}
