using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarFormTypeSO : StagingObjectBase
    {
        public SarFormTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM_TYPE, bool>>> listSarFormTypeExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM_TYPE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_TYPE, bool>>> listVSarFormTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_TYPE, bool>>>();
    }
}
