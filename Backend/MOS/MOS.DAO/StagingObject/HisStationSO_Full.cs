using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisStationSO : StagingObjectBase
    {
        public HisStationSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_STATION, bool>>> listHisStationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_STATION, bool>>> listVHisStationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_STATION, bool>>>();
    }
}
