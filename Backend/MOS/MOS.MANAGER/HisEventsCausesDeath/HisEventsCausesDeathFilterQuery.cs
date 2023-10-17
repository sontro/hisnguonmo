using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    public class HisEventsCausesDeathFilterQuery : HisEventsCausesDeathFilter
    {
        public HisEventsCausesDeathFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EVENTS_CAUSES_DEATH, bool>>> listHisEventsCausesDeathExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EVENTS_CAUSES_DEATH, bool>>>();

        

        internal HisEventsCausesDeathSO Query()
        {
            HisEventsCausesDeathSO search = new HisEventsCausesDeathSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEventsCausesDeathExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEventsCausesDeathExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEventsCausesDeathExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEventsCausesDeathExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.SEVERE_ILLNESS_INFO_ID.HasValue)
                {
                    listHisEventsCausesDeathExpression.Add(o => o.SEVERE_ILLNESS_INFO_ID == this.SEVERE_ILLNESS_INFO_ID.Value);
                }
                if (this.SEVERE_ILLNESS_INFO_IDs != null && this.SEVERE_ILLNESS_INFO_IDs.Count > 0)
                {
                    listHisEventsCausesDeathExpression.Add(o => this.SEVERE_ILLNESS_INFO_IDs.Contains(o.SEVERE_ILLNESS_INFO_ID));
                }
                search.listHisEventsCausesDeathExpression.AddRange(listHisEventsCausesDeathExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEventsCausesDeathExpression.Clear();
                search.listHisEventsCausesDeathExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
