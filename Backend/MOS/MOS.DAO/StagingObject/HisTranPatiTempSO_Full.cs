using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTranPatiTempSO : StagingObjectBase
    {
        public HisTranPatiTempSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TEMP, bool>>> listHisTranPatiTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TEMP, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_TRAN_PATI_TEMP, bool>>> listVHisTranPatiTempExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TRAN_PATI_TEMP, bool>>>();
    }
}
