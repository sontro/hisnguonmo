using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisVaccinationSO : StagingObjectBase
    {
        public HisVaccinationSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION, bool>>> listHisVaccinationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION, bool>>> listVHisVaccinationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION, bool>>>();
    }
}
