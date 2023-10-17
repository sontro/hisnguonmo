using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceSame
{
    public class HisServiceSameFilterQuery : HisServiceSameFilter
    {
        public HisServiceSameFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_SAME, bool>>> listHisServiceSameExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_SAME, bool>>>();

        

        internal HisServiceSameSO Query()
        {
            HisServiceSameSO search = new HisServiceSameSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisServiceSameExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceSameExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceSameExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceSameExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisServiceSameExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisServiceSameExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SAME_ID.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.SAME_ID == this.SAME_ID.Value);
                }
                if (this.SAME_IDs != null)
                {
                    listHisServiceSameExpression.Add(o => this.SAME_IDs.Contains(o.SAME_ID));
                }
                if (this.SERVICE_ID__OR__SAME_ID.HasValue)
                {
                    listHisServiceSameExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID__OR__SAME_ID.Value || o.SAME_ID == this.SERVICE_ID__OR__SAME_ID.Value);
                }
                if (this.SERVICE_ID__OR__SAME_IDs != null)
                {
                    listHisServiceSameExpression.Add(o => this.SERVICE_ID__OR__SAME_IDs.Contains(o.SERVICE_ID) || this.SERVICE_ID__OR__SAME_IDs.Contains(o.SAME_ID));
                }

                search.listHisServiceSameExpression.AddRange(listHisServiceSameExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceSameExpression.Clear();
                search.listHisServiceSameExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
