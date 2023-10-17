using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergyCard
{
    public class HisAllergyCardFilterQuery : HisAllergyCardFilter
    {
        public HisAllergyCardFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ALLERGY_CARD, bool>>> listHisAllergyCardExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ALLERGY_CARD, bool>>>();

        

        internal HisAllergyCardSO Query()
        {
            HisAllergyCardSO search = new HisAllergyCardSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAllergyCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAllergyCardExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAllergyCardExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAllergyCardExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisAllergyCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisAllergyCardExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.ISSUE_TIME_FROM.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.ISSUE_TIME >= this.ISSUE_TIME_FROM.Value);
                }
                if (this.ISSUE_TIME_TO.HasValue)
                {
                    listHisAllergyCardExpression.Add(o => o.ISSUE_TIME <= this.ISSUE_TIME_TO.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.DIAGNOSE_LOGINNAME__EXACT))
                {
                    listHisAllergyCardExpression.Add(o => o.DIAGNOSE_LOGINNAME == this.DIAGNOSE_LOGINNAME__EXACT);
                }

                search.listHisAllergyCardExpression.AddRange(listHisAllergyCardExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAllergyCardExpression.Clear();
                search.listHisAllergyCardExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
