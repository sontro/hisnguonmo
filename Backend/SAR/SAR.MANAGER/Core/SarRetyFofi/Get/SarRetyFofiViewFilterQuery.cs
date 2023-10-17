using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using SAR.Filter;
using SAR.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarRetyFofi.Get
{
    public class SarRetyFofiViewFilterQuery : SarRetyFofiViewFilter
    {
        public SarRetyFofiViewFilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_SAR_RETY_FOFI, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<V_SAR_RETY_FOFI, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal SarRetyFofiSO Query()
        {
            SarRetyFofiSO search = new SarRetyFofiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_FROM__GREATER.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value > this.CREATE_TIME_FROM__GREATER.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.CREATE_TIME_TO__LESS.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value < this.CREATE_TIME_TO__LESS.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_FROM__GREATER.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value > this.MODIFY_TIME_FROM__GREATER.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_TO__LESS.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value < this.MODIFY_TIME_TO__LESS.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null && this.IDs.Count > 0)
                {
                    listExpression.Add(o => this.IDs.Contains(o.ID));
                }

                if (!String.IsNullOrEmpty(this.DESCRIPTION))
                {
                    listExpression.Add(o => o.DESCRIPTION == this.DESCRIPTION);
                }
                if (!String.IsNullOrEmpty(this.JSON_OUTPUT))
                {
                    listExpression.Add(o => o.JSON_OUTPUT == this.JSON_OUTPUT);
                }
                if (!String.IsNullOrEmpty(this.REPORT_TYPE_CODE))
                {
                    listExpression.Add(o => o.REPORT_TYPE_CODE == this.REPORT_TYPE_CODE);
                }
                if (!String.IsNullOrEmpty(this.REPORT_TYPE_NAME))
                {
                    listExpression.Add(o => o.REPORT_TYPE_NAME == this.REPORT_TYPE_NAME);
                }
                if (!String.IsNullOrEmpty(this.FORM_FIELD_CODE))
                {
                    listExpression.Add(o => o.FORM_FIELD_CODE == this.FORM_FIELD_CODE);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.DESCRIPTION.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.JSON_OUTPUT.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.REPORT_TYPE_CODE.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.REPORT_TYPE_NAME.ToLower().Contains(this.KEY_WORD.Trim()) ||
                        o.FORM_FIELD_CODE.ToLower().Contains(this.KEY_WORD.Trim()));
                }

                #endregion
                if (this.REPORT_TYPE_ID.HasValue)
                {
                    listExpression.Add(o => o.REPORT_TYPE_ID == this.REPORT_TYPE_ID.Value);
                }

                search.listVSarRetyFofiExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<V_SAR_RETY_FOFI>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVSarRetyFofiExpression.Clear();
                search.listVSarRetyFofiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
