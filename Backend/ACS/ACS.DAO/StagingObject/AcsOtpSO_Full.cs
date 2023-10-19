using ACS.DAO.Base;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.DAO.StagingObject
{
    public class AcsOtpSO : StagingObjectBase
    {
        public AcsOtpSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<ACS_OTP, bool>>> listAcsOtpExpression = new List<System.Linq.Expressions.Expression<Func<ACS_OTP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_ACS_OTP, bool>>> listVAcsOtpExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_OTP, bool>>>();
    }
}
