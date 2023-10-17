using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisHospitalizeReasonSO : StagingObjectBase
    {
        public HisHospitalizeReasonSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_HOSPITALIZE_REASON, bool>>> listHisHospitalizeReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HOSPITALIZE_REASON, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_HOSPITALIZE_REASON, bool>>> listVHisHospitalizeReasonExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HOSPITALIZE_REASON, bool>>>();
    }
}
