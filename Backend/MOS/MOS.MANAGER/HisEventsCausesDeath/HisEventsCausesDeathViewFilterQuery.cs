using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    public class HisEventsCausesDeathViewFilterQuery : HisEventsCausesDeathViewFilter
    {
        public HisEventsCausesDeathViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EVENTS_CAUSES_DEATH, bool>>> listVHisEventsCausesDeathExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EVENTS_CAUSES_DEATH, bool>>>();

        

        internal HisEventsCausesDeathSO Query()
        {
            HisEventsCausesDeathSO search = new HisEventsCausesDeathSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisEventsCausesDeathExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEventsCausesDeathExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisEventsCausesDeathExpression.AddRange(listVHisEventsCausesDeathExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEventsCausesDeathExpression.Clear();
                search.listVHisEventsCausesDeathExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
