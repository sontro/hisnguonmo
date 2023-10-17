using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRoomTimeSO : StagingObjectBase
    {
        public HisRoomTimeSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ROOM_TIME, bool>>> listHisRoomTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ROOM_TIME, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_TIME, bool>>> listVHisRoomTimeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_TIME, bool>>>();
    }
}
