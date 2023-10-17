using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    public class HisStorageConditionFilterQuery : HisStorageConditionFilter
    {
        public HisStorageConditionFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_STORAGE_CONDITION, bool>>> listHisStorageConditionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STORAGE_CONDITION, bool>>>();

        

        internal HisStorageConditionSO Query()
        {
            HisStorageConditionSO search = new HisStorageConditionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisStorageConditionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisStorageConditionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisStorageConditionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisStorageConditionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisStorageConditionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisStorageConditionExpression.Add(o => 
                        o.STORAGE_CONDITION_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.STORAGE_CONDITION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisStorageConditionExpression.AddRange(listHisStorageConditionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisStorageConditionExpression.Clear();
                search.listHisStorageConditionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
