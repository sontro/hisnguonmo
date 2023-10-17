using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMediRecordTypeSO : StagingObjectBase
    {
        public HisMediRecordTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_TYPE, bool>>> listHisMediRecordTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_TYPE, bool>>>();
    }
}
