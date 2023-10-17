using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSevereIllnessInfoSO : StagingObjectBase
    {
        public HisSevereIllnessInfoSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SEVERE_ILLNESS_INFO, bool>>> listHisSevereIllnessInfoExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SEVERE_ILLNESS_INFO, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SEVERE_ILLNESS_INFO, bool>>> listVHisSevereIllnessInfoExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SEVERE_ILLNESS_INFO, bool>>>();
    }
}
