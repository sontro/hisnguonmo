using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisAllergyCardSO : StagingObjectBase
    {
        public HisAllergyCardSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_ALLERGY_CARD, bool>>> listHisAllergyCardExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ALLERGY_CARD, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGY_CARD, bool>>> listVHisAllergyCardExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGY_CARD, bool>>>();
    }
}
