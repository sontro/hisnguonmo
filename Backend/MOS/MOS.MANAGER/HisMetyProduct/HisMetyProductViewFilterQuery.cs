using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    public class HisMetyProductViewFilterQuery : HisMetyProductViewFilter
    {
        public HisMetyProductViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_METY_PRODUCT, bool>>> listVHisMetyProductExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_METY_PRODUCT, bool>>>();



        internal HisMetyProductSO Query()
        {
            HisMetyProductSO search = new HisMetyProductSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMetyProductExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMetyProductExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMetyProductExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMetyProductExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMetyProductExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisMetyProductExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.MEDICINE_LINE_ID.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.MEDICINE_LINE_ID.HasValue && o.MEDICINE_LINE_ID.Value == this.MEDICINE_LINE_ID.Value);
                }
                if (this.MEDICINE_LINE_IDs != null)
                {
                    listVHisMetyProductExpression.Add(o => o.MEDICINE_LINE_ID.HasValue && this.MEDICINE_LINE_IDs.Contains(o.MEDICINE_LINE_ID.Value));
                }
                if (this.TDL_SERVICE_UNIT_ID.HasValue)
                {
                    listVHisMetyProductExpression.Add(o => o.TDL_SERVICE_UNIT_ID == this.TDL_SERVICE_UNIT_ID.Value);
                }
                if (this.TDL_SERVICE_UNIT_IDs != null)
                {
                    listVHisMetyProductExpression.Add(o => this.TDL_SERVICE_UNIT_IDs.Contains(o.TDL_SERVICE_UNIT_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.MEDICINE_LINE_CODE__EXACT))
                {
                    listVHisMetyProductExpression.Add(o => o.MEDICINE_LINE_CODE == this.MEDICINE_LINE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.MEDICINE_TYPE_CODE__EXACT))
                {
                    listVHisMetyProductExpression.Add(o => o.MEDICINE_TYPE_CODE == this.MEDICINE_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_UNIT_CODE__EXACT))
                {
                    listVHisMetyProductExpression.Add(o => o.SERVICE_UNIT_CODE == this.SERVICE_UNIT_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMetyProductExpression.Add(o => o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_LINE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_LINE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REGISTER_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisMetyProductExpression.AddRange(listVHisMetyProductExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMetyProductExpression.Clear();
                search.listVHisMetyProductExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
