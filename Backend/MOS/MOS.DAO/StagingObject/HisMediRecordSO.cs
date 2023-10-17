using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMediRecordSO : StagingObjectBase
    {
        public HisMediRecordSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD, bool>>> listHisMediRecordExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD, bool>>> listVHisMediRecordExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_1, bool>>> listVHisMediRecord1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_1, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_2, bool>>> listVHisMediRecord2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_2, bool>>>();
    }
}
