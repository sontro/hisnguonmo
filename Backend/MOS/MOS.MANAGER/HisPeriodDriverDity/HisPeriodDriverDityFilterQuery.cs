using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    public class HisPeriodDriverDityFilterQuery : HisPeriodDriverDityFilter
    {
        public HisPeriodDriverDityFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PERIOD_DRIVER_DITY, bool>>> listHisPeriodDriverDityExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PERIOD_DRIVER_DITY, bool>>>();

        

        internal HisPeriodDriverDitySO Query()
        {
            HisPeriodDriverDitySO search = new HisPeriodDriverDitySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPeriodDriverDityExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPeriodDriverDityExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPeriodDriverDityExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPeriodDriverDityExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.KSK_PERIOD_DRIVER_ID.HasValue)
                {
                    listHisPeriodDriverDityExpression.Add(o => o.KSK_PERIOD_DRIVER_ID == this.KSK_PERIOD_DRIVER_ID.Value);
                }
                
                search.listHisPeriodDriverDityExpression.AddRange(listHisPeriodDriverDityExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPeriodDriverDityExpression.Clear();
                search.listHisPeriodDriverDityExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
