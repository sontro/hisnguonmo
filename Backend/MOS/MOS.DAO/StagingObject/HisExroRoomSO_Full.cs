using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisExroRoomSO : StagingObjectBase
    {
        public HisExroRoomSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EXRO_ROOM, bool>>> listHisExroRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXRO_ROOM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EXRO_ROOM, bool>>> listVHisExroRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXRO_ROOM, bool>>>();
    }
}
