using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    public class HisServiceConditionFilterQuery : HisServiceConditionFilter
    {
        public HisServiceConditionFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CONDITION, bool>>> listHisServiceConditionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_CONDITION, bool>>>();



        internal HisServiceConditionSO Query()
        {
            HisServiceConditionSO search = new HisServiceConditionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisServiceConditionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceConditionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceConditionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceConditionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisServiceConditionExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisServiceConditionExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisServiceConditionExpression.Add(o =>
                        o.SERVICE_CONDITION_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_CONDITION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisServiceConditionExpression.AddRange(listHisServiceConditionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceConditionExpression.Clear();
                search.listHisServiceConditionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
