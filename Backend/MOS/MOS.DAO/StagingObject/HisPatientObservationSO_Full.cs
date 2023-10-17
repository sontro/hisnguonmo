using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisPatientObservationSO : StagingObjectBase
    {
        public HisPatientObservationSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_OBSERVATION, bool>>> listHisPatientObservationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_OBSERVATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_OBSERVATION, bool>>> listVHisPatientObservationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_OBSERVATION, bool>>>();
    }
}
