using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmotionlessResult
{
    public class HisEmotionlessResultFilterQuery : HisEmotionlessResultFilter
    {
        public HisEmotionlessResultFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EMOTIONLESS_RESULT, bool>>> listHisEmotionlessResultExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMOTIONLESS_RESULT, bool>>>();

        

        internal HisEmotionlessResultSO Query()
        {
            HisEmotionlessResultSO search = new HisEmotionlessResultSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEmotionlessResultExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEmotionlessResultExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEmotionlessResultExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEmotionlessResultExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEmotionlessResultExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisEmotionlessResultExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.EMOTIONLESS_RESULT_CODE__EXACT))
                {
                    listHisEmotionlessResultExpression.Add(o => o.EMOTIONLESS_RESULT_CODE == this.EMOTIONLESS_RESULT_CODE__EXACT);
                }

                search.listHisEmotionlessResultExpression.AddRange(listHisEmotionlessResultExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEmotionlessResultExpression.Clear();
                search.listHisEmotionlessResultExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
