using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceSame
{
    public class HisServiceSameViewFilterQuery : HisServiceSameViewFilter
    {
        public HisServiceSameViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_SAME, bool>>> listVHisServiceSameExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_SAME, bool>>>();

        

        internal HisServiceSameSO Query()
        {
            HisServiceSameSO search = new HisServiceSameSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisServiceSameExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceSameExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceSameExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceSameExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisServiceSameExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisServiceSameExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SAME_ID.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.SAME_ID == this.SAME_ID.Value);
                }
                if (this.SAME_IDs != null)
                {
                    listVHisServiceSameExpression.Add(o => this.SAME_IDs.Contains(o.SAME_ID));
                }
                if (this.SERVICE_ID__OR__SAME_ID.HasValue)
                {
                    listVHisServiceSameExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID__OR__SAME_ID.Value || o.SAME_ID == this.SERVICE_ID__OR__SAME_ID.Value);
                }
                if (this.SERVICE_ID__OR__SAME_IDs != null)
                {
                    listVHisServiceSameExpression.Add(o => this.SERVICE_ID__OR__SAME_IDs.Contains(o.SERVICE_ID) || this.SERVICE_ID__OR__SAME_IDs.Contains(o.SAME_ID));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceSameExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.SAME_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SAME_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SAME_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SAME_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisServiceSameExpression.AddRange(listVHisServiceSameExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceSameExpression.Clear();
                search.listVHisServiceSameExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
