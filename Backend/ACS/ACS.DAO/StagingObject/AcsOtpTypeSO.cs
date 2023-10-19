using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsOtpTypeSO : StagingObjectBase
    {
        public AcsOtpTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_OTP_TYPE, bool>>> listAcsOtpTypeExpression = new List<System.Linq.Expressions.Expression<Func<ACS_OTP_TYPE, bool>>>();
    }
}
