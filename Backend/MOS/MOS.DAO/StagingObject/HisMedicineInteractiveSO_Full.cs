using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisMedicineInteractiveSO : StagingObjectBase
    {
        public HisMedicineInteractiveSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_INTERACTIVE, bool>>> listHisMedicineInteractiveExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_INTERACTIVE, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_INTERACTIVE, bool>>> listVHisMedicineInteractiveExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_INTERACTIVE, bool>>>();
    }
}
