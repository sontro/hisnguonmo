using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    public class HisKskPeriodDriverFilterQuery : HisKskPeriodDriverFilter
    {
        public HisKskPeriodDriverFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_PERIOD_DRIVER, bool>>> listHisKskPeriodDriverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_PERIOD_DRIVER, bool>>>();

        

        internal HisKskPeriodDriverSO Query()
        {
            HisKskPeriodDriverSO search = new HisKskPeriodDriverSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisKskPeriodDriverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskPeriodDriverExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskPeriodDriverExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskPeriodDriverExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listHisKskPeriodDriverExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.LICENSE_CLASS_ID.HasValue)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.LICENSE_CLASS_ID == this.LICENSE_CLASS_ID.Value);
                }
                if (this.LICENSE_CLASS_IDs != null)
                {
                    listHisKskPeriodDriverExpression.Add(o => o.LICENSE_CLASS_ID.HasValue && this.LICENSE_CLASS_IDs.Contains(o.LICENSE_CLASS_ID.Value));
                }

                search.listHisKskPeriodDriverExpression.AddRange(listHisKskPeriodDriverExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskPeriodDriverExpression.Clear();
                search.listHisKskPeriodDriverExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
