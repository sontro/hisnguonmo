using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfig
{
    public class HisConfigViewFilterQuery : HisConfigViewFilter
    {
        public HisConfigViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CONFIG, bool>>> listVHisConfigExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CONFIG, bool>>>();



        internal HisConfigSO Query()
        {
            HisConfigSO search = new HisConfigSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisConfigExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisConfigExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisConfigExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisConfigExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisConfigExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisConfigExpression.Add(o => o.BRANCH_ID.HasValue && o.BRANCH_ID.Value == this.BRANCH_ID.Value);
                }
                if (this.NULL__OR__BRANCH_ID.HasValue)
                {
                    listVHisConfigExpression.Add(o => !o.BRANCH_ID.HasValue || o.BRANCH_ID.Value == this.NULL__OR__BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisConfigExpression.Add(o => o.BRANCH_ID.HasValue && this.BRANCH_IDs.Contains(o.BRANCH_ID.Value));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisConfigExpression.Add(o => o.BRANCH_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.BRANCH_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DEFAULT_VALUE.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.KEY.ToLower().Contains(this.KEY_WORD)
                        || o.VALUE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisConfigExpression.AddRange(listVHisConfigExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisConfigExpression.Clear();
                search.listVHisConfigExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
