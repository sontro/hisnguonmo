using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    public class HisMestPeriodBltyViewFilterQuery : HisMestPeriodBltyViewFilter
    {
        public HisMestPeriodBltyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_BLTY, bool>>> listVHisMestPeriodBltyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_BLTY, bool>>>();

        

        internal HisMestPeriodBltySO Query()
        {
            HisMestPeriodBltySO search = new HisMestPeriodBltySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMestPeriodBltyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMestPeriodBltyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDI_STOCK_PERIOD_ID.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.MEDI_STOCK_PERIOD_ID == this.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.MEDI_STOCK_PERIOD_IDs != null)
                {
                    listVHisMestPeriodBltyExpression.Add(o => this.MEDI_STOCK_PERIOD_IDs.Contains(o.MEDI_STOCK_PERIOD_ID));
                }
                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listVHisMestPeriodBltyExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.IS_ERROR.HasValue && this.IS_ERROR.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.IN_AMOUNT < 0 || o.OUT_AMOUNT < 0 || o.BEGIN_AMOUNT < 0 || o.VIR_END_AMOUNT < 0 || o.INVENTORY_AMOUNT < 0 || o.VIR_END_AMOUNT != o.INVENTORY_AMOUNT);
                }
                if (this.IS_ERROR.HasValue && !this.IS_ERROR.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.IN_AMOUNT >= 0 && o.OUT_AMOUNT >= 0 && o.BEGIN_AMOUNT >= 0 && o.VIR_END_AMOUNT >= 0 && o.INVENTORY_AMOUNT >= 0 && o.VIR_END_AMOUNT == o.INVENTORY_AMOUNT);
                }
                if (this.IS_ERROR_NOT_INVENTORY.HasValue && this.IS_ERROR_NOT_INVENTORY.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.IN_AMOUNT < 0 || o.OUT_AMOUNT < 0 || o.BEGIN_AMOUNT < 0 || o.VIR_END_AMOUNT < 0 );
                }
                if (this.IS_ERROR_NOT_INVENTORY.HasValue && !this.IS_ERROR_NOT_INVENTORY.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.IN_AMOUNT >= 0 && o.OUT_AMOUNT >= 0 && o.BEGIN_AMOUNT >= 0 && o.VIR_END_AMOUNT >= 0 );
                }
                if (this.IS_EMPTY.HasValue && this.IS_EMPTY.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.VIR_END_AMOUNT == 0);
                }
                if (this.IS_EMPTY.HasValue && !this.IS_EMPTY.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.VIR_END_AMOUNT != 0);
                }
                if (this.IS_NO_CHANGE.HasValue && this.IS_NO_CHANGE.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.BEGIN_AMOUNT == o.VIR_END_AMOUNT);
                }
                if (this.IS_NO_CHANGE.HasValue && !this.IS_NO_CHANGE.Value)
                {
                    listVHisMestPeriodBltyExpression.Add(o => o.BEGIN_AMOUNT != o.VIR_END_AMOUNT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMestPeriodBltyExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDI_STOCK_PERIOD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BLOOD_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BLOOD_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BLOOD_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PACKING_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisMestPeriodBltyExpression.AddRange(listVHisMestPeriodBltyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMestPeriodBltyExpression.Clear();
                search.listVHisMestPeriodBltyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
