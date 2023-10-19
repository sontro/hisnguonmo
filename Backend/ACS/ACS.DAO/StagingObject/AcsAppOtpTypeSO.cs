using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsAppOtpTypeSO : StagingObjectBase
    {
        public AcsAppOtpTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_APP_OTP_TYPE, bool>>> listAcsAppOtpTypeExpression = new List<System.Linq.Expressions.Expression<Func<ACS_APP_OTP_TYPE, bool>>>();
    }
}
