using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisObeyContraindiSO : StagingObjectBase
    {
        public HisObeyContraindiSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_OBEY_CONTRAINDI, bool>>> listHisObeyContraindiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_OBEY_CONTRAINDI, bool>>>();
    }
}
