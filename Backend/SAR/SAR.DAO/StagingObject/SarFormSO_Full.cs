using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarFormSO : StagingObjectBase
    {
        public SarFormSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM, bool>>> listSarFormExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_FORM, bool>>> listVSarFormExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_FORM, bool>>>();
    }
}
