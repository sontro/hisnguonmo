using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisKskDriverSO : StagingObjectBase
    {
        public HisKskDriverSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_KSK_DRIVER, bool>>> listHisKskDriverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_DRIVER, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_DRIVER, bool>>> listVHisKskDriverExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_DRIVER, bool>>>();
    }
}
