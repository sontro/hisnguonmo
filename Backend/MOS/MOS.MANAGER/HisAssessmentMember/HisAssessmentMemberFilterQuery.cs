using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using Inventec.Core;

namespace MOS.MANAGER.HisAssessmentMember
{
    public class HisAssessmentMemberFilterQuery : HisAssessmentMemberFilter
    {
        public HisAssessmentMemberFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_MEMBER, bool>>> listHisAssessmentMemberExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ASSESSMENT_MEMBER, bool>>>();

        

        internal HisAssessmentMemberSO Query()
        {
            HisAssessmentMemberSO search = new HisAssessmentMemberSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAssessmentMemberExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAssessmentMemberExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAssessmentMemberExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAssessmentMemberExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDICAL_ASSESSMENT_ID.HasValue)
                {
                    listHisAssessmentMemberExpression.Add(o => o.MEDICAL_ASSESSMENT_ID == this.MEDICAL_ASSESSMENT_ID.Value);
                }
                if (this.MEDICAL_ASSESSMENT_IDs != null && this.MEDICAL_ASSESSMENT_IDs.Count > 0)
                {
                    listHisAssessmentMemberExpression.Add(o => o.MEDICAL_ASSESSMENT_ID != null && this.MEDICAL_ASSESSMENT_IDs.Contains(o.MEDICAL_ASSESSMENT_ID));
                }
                
                search.listHisAssessmentMemberExpression.AddRange(listHisAssessmentMemberExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAssessmentMemberExpression.Clear();
                search.listHisAssessmentMemberExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
