using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisFileTypeSO : StagingObjectBase
    {
        public HisFileTypeSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_FILE_TYPE, bool>>> listHisFileTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FILE_TYPE, bool>>>();
    }
}
