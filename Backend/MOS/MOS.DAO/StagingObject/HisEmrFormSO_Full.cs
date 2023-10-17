using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEmrFormSO : StagingObjectBase
    {
        public HisEmrFormSO()
        {
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EMR_FORM, bool>>> listHisEmrFormExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMR_FORM, bool>>>();
        public List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_FORM, bool>>> listVHisEmrFormExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_FORM, bool>>>();
    }
}
