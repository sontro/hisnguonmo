using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using SAR.Filter;
using SAR.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintLog.Get
{
    public class SarPrintLogFilterQuery : SarPrintLogFilter
    {
        public SarPrintLogFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SAR_PRINT_LOG, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_LOG, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal SarPrintLogSO Query()
        {
            SarPrintLogSO search = new SarPrintLogSO();
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
                #endregion

                if (!String.IsNullOrWhiteSpace(this.PRINT_TYPE_CODE__EXACT))
                {
                    listExpression.Add(o => o.PRINT_TYPE_CODE == this.PRINT_TYPE_CODE__EXACT);
                }
                if (this.PRINT_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.PRINT_TIME.Value >= this.PRINT_TIME_FROM.Value);
                }
                if (this.PRINT_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.PRINT_TIME.Value <= this.PRINT_TIME_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.UNIQUE_CODE__EXACT))
                {
                    listExpression.Add(o => o.UNIQUE_CODE == this.UNIQUE_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.IP.ToLower().Contains(this.KEY_WORD) ||
                        o.UNIQUE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PRINT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DATA_CONTENT.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listSarPrintLogExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<SAR_PRINT_LOG>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSarPrintLogExpression.Clear();
                search.listSarPrintLogExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}