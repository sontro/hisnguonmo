using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskGeneralSO : StagingObjectBase
    {
        public HisKskGeneralSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_GENERAL, bool>>> listHisKskGeneralExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_GENERAL, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_GENERAL, bool>>> listVHisKskGeneralExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_GENERAL, bool>>>();
    }
}
