using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisRoomSaroSO : StagingObjectBase
    {
        public HisRoomSaroSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ROOM_SARO, bool>>> listHisRoomSaroExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ROOM_SARO, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_SARO, bool>>> listVHisRoomSaroExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ROOM_SARO, bool>>>();
    }
}
