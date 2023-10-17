using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    public class HisKskOtherFilterQuery : HisKskOtherFilter
    {
        public HisKskOtherFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_OTHER, bool>>> listHisKskOtherExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_OTHER, bool>>>();

        

        internal HisKskOtherSO Query()
        {
            HisKskOtherSO search = new HisKskOtherSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisKskOtherExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskOtherExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskOtherExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskOtherExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listHisKskOtherExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }

                search.listHisKskOtherExpression.AddRange(listHisKskOtherExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskOtherExpression.Clear();
                search.listHisKskOtherExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
