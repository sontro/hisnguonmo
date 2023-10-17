using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPatientTypeRoomSO : StagingObjectBase
    {
        public HisPatientTypeRoomSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_TYPE_ROOM, bool>>> listHisPatientTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_TYPE_ROOM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_TYPE_ROOM, bool>>> listVHisPatientTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_TYPE_ROOM, bool>>>();
    }
}
