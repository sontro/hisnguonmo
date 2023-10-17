using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SAR.DAO.StagingObject
{
    public class SarFormDataSO : StagingObjectBase
    {
        public SarFormDataSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM_DATA, bool>>> listSarFormDataExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM_DATA, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_DATA, bool>>> listVSarFormDataExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_FORM_DATA, bool>>>();
    }
}
