using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    public class HisInteractiveGradeFilterQuery : HisInteractiveGradeFilter
    {
        public HisInteractiveGradeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_INTERACTIVE_GRADE, bool>>> listHisInteractiveGradeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_INTERACTIVE_GRADE, bool>>>();



        internal HisInteractiveGradeSO Query()
        {
            HisInteractiveGradeSO search = new HisInteractiveGradeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisInteractiveGradeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisInteractiveGradeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisInteractiveGradeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisInteractiveGradeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.INTERACTIVE_GRADE.HasValue)
                {
                    listHisInteractiveGradeExpression.Add(o => o.INTERACTIVE_GRADE == this.INTERACTIVE_GRADE.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisInteractiveGradeExpression.Add(o =>
                        o.INTERACTIVE_GRADE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisInteractiveGradeExpression.AddRange(listHisInteractiveGradeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisInteractiveGradeExpression.Clear();
                search.listHisInteractiveGradeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
