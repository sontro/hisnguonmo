using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    public class HisMestPeriodBloodViewFilterQuery : HisMestPeriodBloodViewFilter
    {
        public HisMestPeriodBloodViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_BLOOD, bool>>> listVHisMestPeriodBloodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_BLOOD, bool>>>();

        

        internal HisMestPeriodBloodSO Query()
        {
            HisMestPeriodBloodSO search = new HisMestPeriodBloodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMestPeriodBloodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMestPeriodBloodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                if (this.MEDI_STOCK_PERIOD_ID.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.MEDI_STOCK_PERIOD_ID == this.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listVHisMestPeriodBloodExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }

                search.listVHisMestPeriodBloodExpression.AddRange(listVHisMestPeriodBloodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMestPeriodBloodExpression.Clear();
                search.listVHisMestPeriodBloodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
