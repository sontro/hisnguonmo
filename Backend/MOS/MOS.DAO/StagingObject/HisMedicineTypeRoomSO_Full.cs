using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineTypeRoomSO : StagingObjectBase
    {
        public HisMedicineTypeRoomSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_TYPE_ROOM, bool>>> listHisMedicineTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_TYPE_ROOM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_ROOM, bool>>> listVHisMedicineTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_ROOM, bool>>>();
    }
}
