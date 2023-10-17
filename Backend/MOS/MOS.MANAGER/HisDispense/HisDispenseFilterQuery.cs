using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense
{
    public class HisDispenseFilterQuery : HisDispenseFilter
    {
        public HisDispenseFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DISPENSE, bool>>> listHisDispenseExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DISPENSE, bool>>>();

        internal HisDispenseSO Query()
        {
            HisDispenseSO search = new HisDispenseSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisDispenseExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDispenseExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDispenseExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDispenseExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisDispenseExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listHisDispenseExpression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.DISPENSE_TYPE_ID.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.DISPENSE_TYPE_ID == this.DISPENSE_TYPE_ID.Value);
                }
                if (this.DISPENSE_TYPE_IDs != null)
                {
                    listHisDispenseExpression.Add(o => this.DISPENSE_TYPE_IDs.Contains(o.DISPENSE_TYPE_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.DISPENSE_CODE__EXACT))
                {
                    listHisDispenseExpression.Add(o => o.DISPENSE_CODE == this.DISPENSE_CODE__EXACT);
                }
                if (this.DISPENSE_DATE_FROM.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.DISPENSE_DATE >= this.DISPENSE_DATE_FROM.Value);
                }
                if (this.DISPENSE_DATE_TO.HasValue)
                {
                    listHisDispenseExpression.Add(o => o.DISPENSE_DATE <= this.DISPENSE_DATE_TO.Value);
                }

                search.listHisDispenseExpression.AddRange(listHisDispenseExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDispenseExpression.Clear();
                search.listHisDispenseExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
