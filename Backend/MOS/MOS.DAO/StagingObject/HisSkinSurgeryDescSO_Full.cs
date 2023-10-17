using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSkinSurgeryDescSO : StagingObjectBase
    {
        public HisSkinSurgeryDescSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SKIN_SURGERY_DESC, bool>>> listHisSkinSurgeryDescExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SKIN_SURGERY_DESC, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SKIN_SURGERY_DESC, bool>>> listVHisSkinSurgeryDescExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SKIN_SURGERY_DESC, bool>>>();
    }
}
