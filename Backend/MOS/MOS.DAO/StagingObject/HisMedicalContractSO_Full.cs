using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicalContractSO : StagingObjectBase
    {
        public HisMedicalContractSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_CONTRACT, bool>>> listHisMedicalContractExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_CONTRACT, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_CONTRACT, bool>>> listVHisMedicalContractExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_CONTRACT, bool>>>();
    }
}
