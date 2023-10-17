using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineMaterialSO : StagingObjectBase
    {
        public HisMedicineMaterialSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MATERIAL, bool>>> listHisMedicineMaterialExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MATERIAL, bool>>>();

        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MATERIAL, bool>>> listVHisMedicineMaterialExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MATERIAL, bool>>>();
    }
}
