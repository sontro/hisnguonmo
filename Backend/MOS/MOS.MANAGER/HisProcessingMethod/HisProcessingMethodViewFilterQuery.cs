using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    public class HisProcessingMethodViewFilterQuery : HisProcessingMethodViewFilter
    {
        public HisProcessingMethodViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PROCESSING_METHOD, bool>>> listVHisProcessingMethodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PROCESSING_METHOD, bool>>>();

        

        internal HisProcessingMethodSO Query()
        {
            HisProcessingMethodSO search = new HisProcessingMethodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisProcessingMethodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisProcessingMethodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisProcessingMethodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisProcessingMethodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisProcessingMethodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisProcessingMethodExpression.AddRange(listVHisProcessingMethodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisProcessingMethodExpression.Clear();
                search.listVHisProcessingMethodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
