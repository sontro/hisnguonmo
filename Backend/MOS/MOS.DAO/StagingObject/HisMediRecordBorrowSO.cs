using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMediRecordBorrowSO : StagingObjectBase
    {
        public HisMediRecordBorrowSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_BORROW, bool>>> listHisMediRecordBorrowExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_BORROW, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_BORROW, bool>>> listVHisMediRecordBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_BORROW, bool>>>();
    }
}
