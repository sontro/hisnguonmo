using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSurgRemunerationSO : StagingObjectBase
    {
        public HisSurgRemunerationSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMUNERATION, bool>>> listHisSurgRemunerationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMUNERATION, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMUNERATION, bool>>> listVHisSurgRemunerationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SURG_REMUNERATION, bool>>>();
    }
}
