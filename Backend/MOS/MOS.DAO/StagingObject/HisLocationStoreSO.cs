using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisLocationStoreSO : StagingObjectBase
    {
        public HisLocationStoreSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_LOCATION_STORE, bool>>> listHisLocationStoreExpression = new List<System.Linq.Expressions.Expression<Func<HIS_LOCATION_STORE, bool>>>();
    }
}
