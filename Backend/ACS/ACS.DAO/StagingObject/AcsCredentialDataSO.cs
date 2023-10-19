using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsCredentialDataSO : StagingObjectBase
    {
        public AcsCredentialDataSO()
        {
            listAcsCredentialDataExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_CREDENTIAL_DATA, bool>>> listAcsCredentialDataExpression = new List<System.Linq.Expressions.Expression<Func<ACS_CREDENTIAL_DATA, bool>>>();
    }
}
