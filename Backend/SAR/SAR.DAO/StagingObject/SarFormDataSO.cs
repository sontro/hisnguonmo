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
            this.Init();
        }

        public SarFormDataSO(bool isIncludeDeleted)
        {
            this.IsIncludeDeleted = isIncludeDeleted;
            this.Init();
        }

        public void Init()
        {
            if (!this.IsIncludeDeleted)
            {
                listSarFormDataExpression.Add(o => (!o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1));
            }
        }

        public List<System.Linq.Expressions.Expression<Func<SAR_FORM_DATA, bool>>> listSarFormDataExpression = new List<System.Linq.Expressions.Expression<Func<SAR_FORM_DATA, bool>>>();
    }
}
