using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    public class HisAssessmentObjectViewFilterQuery : HisAssessmentObjectViewFilter
    {
        public HisAssessmentObjectViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_OBJECT, bool>>> listVHisAssessmentObjectExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ASSESSMENT_OBJECT, bool>>>();

        

        internal HisAssessmentObjectSO Query()
        {
            HisAssessmentObjectSO search = new HisAssessmentObjectSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAssessmentObjectExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAssessmentObjectExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAssessmentObjectExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAssessmentObjectExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAssessmentObjectExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisAssessmentObjectExpression.AddRange(listVHisAssessmentObjectExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAssessmentObjectExpression.Clear();
                search.listVHisAssessmentObjectExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
