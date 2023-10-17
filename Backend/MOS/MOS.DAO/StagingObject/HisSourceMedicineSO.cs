using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisSourceMedicineSO : StagingObjectBase
    {
        public HisSourceMedicineSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_SOURCE_MEDICINE, bool>>> listHisSourceMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SOURCE_MEDICINE, bool>>>();
    }
}
