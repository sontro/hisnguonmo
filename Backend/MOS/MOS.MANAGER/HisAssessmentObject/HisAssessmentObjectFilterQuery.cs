using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    public class HisAssessmentObjectFilterQuery : HisAssessmentObjectFilter
    {
        public HisAssessmentObjectFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_OBJECT, bool>>> listHisAssessmentObjectExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_OBJECT, bool>>>();

        

        internal HisAssessmentObjectSO Query()
        {
            HisAssessmentObjectSO search = new HisAssessmentObjectSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAssessmentObjectExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAssessmentObjectExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAssessmentObjectExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAssessmentObjectExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAssessmentObjectExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisAssessmentObjectExpression.AddRange(listHisAssessmentObjectExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAssessmentObjectExpression.Clear();
                search.listHisAssessmentObjectExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
