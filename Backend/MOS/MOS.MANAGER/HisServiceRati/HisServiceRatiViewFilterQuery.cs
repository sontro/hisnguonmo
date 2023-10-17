using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRati
{
    public class HisServiceRatiViewFilterQuery : HisServiceRatiViewFilter
    {
        public HisServiceRatiViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_RATI, bool>>> listVHisServiceRatiExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_RATI, bool>>>();

        

        internal HisServiceRatiSO Query()
        {
            HisServiceRatiSO search = new HisServiceRatiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisServiceRatiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceRatiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceRatiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceRatiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceRatiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisServiceRatiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisServiceRatiExpression.AddRange(listVHisServiceRatiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceRatiExpression.Clear();
                search.listVHisServiceRatiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
