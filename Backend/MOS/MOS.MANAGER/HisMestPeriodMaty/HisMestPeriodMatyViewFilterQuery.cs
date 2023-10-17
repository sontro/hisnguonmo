using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMaty
{
    public class HisMestPeriodMatyViewFilterQuery : HisMestPeriodMatyViewFilter
    {
        public HisMestPeriodMatyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_MATY, bool>>> listVHisMestPeriodMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PERIOD_MATY, bool>>>();



        internal HisMestPeriodMatySO Query()
        {
            HisMestPeriodMatySO search = new HisMestPeriodMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisMestPeriodMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDI_STOCK_PERIOD_ID.HasValue)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.MEDI_STOCK_PERIOD_ID == this.MEDI_STOCK_PERIOD_ID.Value);
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MEDI_STOCK_PERIOD_IDs != null)
                {
                    listVHisMestPeriodMatyExpression.Add(o => this.MEDI_STOCK_PERIOD_IDs.Contains(o.MEDI_STOCK_PERIOD_ID));
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listVHisMestPeriodMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.IS_ERROR.HasValue && this.IS_ERROR.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.IN_AMOUNT < 0 || o.OUT_AMOUNT < 0 || o.BEGIN_AMOUNT < 0 || o.VIR_END_AMOUNT < 0 || o.INVENTORY_AMOUNT < 0 || o.VIR_END_AMOUNT != o.INVENTORY_AMOUNT);
                }
                if (this.IS_ERROR.HasValue && !this.IS_ERROR.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.IN_AMOUNT >= 0 && o.OUT_AMOUNT >= 0 && o.BEGIN_AMOUNT >= 0 && o.VIR_END_AMOUNT >= 0 && o.INVENTORY_AMOUNT >= 0 && o.VIR_END_AMOUNT == o.INVENTORY_AMOUNT);
                }
                if (this.IS_EMPTY.HasValue && this.IS_EMPTY.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.VIR_END_AMOUNT == 0);
                }
                if (this.IS_EMPTY.HasValue && !this.IS_EMPTY.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.VIR_END_AMOUNT != 0);
                }
                if (this.IS_NO_CHANGE.HasValue && this.IS_NO_CHANGE.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.BEGIN_AMOUNT == o.VIR_END_AMOUNT);
                }
                if (this.IS_NO_CHANGE.HasValue && !this.IS_NO_CHANGE.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.BEGIN_AMOUNT != o.VIR_END_AMOUNT);
                }

                if (this.IS_ERROR_NOT_INVENTORY.HasValue && this.IS_ERROR_NOT_INVENTORY.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.IN_AMOUNT < 0 || o.OUT_AMOUNT < 0 || o.BEGIN_AMOUNT < 0 || o.VIR_END_AMOUNT < 0);
                }
                if (this.IS_ERROR_NOT_INVENTORY.HasValue && !this.IS_ERROR_NOT_INVENTORY.Value)
                {
                    listVHisMestPeriodMatyExpression.Add(o => o.IN_AMOUNT >= 0 && o.OUT_AMOUNT >= 0 && o.BEGIN_AMOUNT >= 0 && o.VIR_END_AMOUNT >= 0);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMestPeriodMatyExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.DESCRIPTION.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDI_STOCK_PERIOD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PACKING_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisMestPeriodMatyExpression.AddRange(listVHisMestPeriodMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMestPeriodMatyExpression.Clear();
                search.listVHisMestPeriodMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
