using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using SAR.Filter;
using SAR.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarPrintTypeCfg.Get
{
    public class SarPrintTypeCfgFilterQuery : SarPrintTypeCfgFilter
    {
        public SarPrintTypeCfgFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE_CFG, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<SAR_PRINT_TYPE_CFG, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal SarPrintTypeCfgSO Query()
        {
            SarPrintTypeCfgSO search = new SarPrintTypeCfgSO();
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

                if (!String.IsNullOrWhiteSpace(this.APP_CODE__EXACT))
                {
                    listExpression.Add(o => o.APP_CODE == this.APP_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.BRANCH_CODE__EXACT))
                {
                    listExpression.Add(o => o.BRANCH_CODE == this.BRANCH_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.CONTROL_CODE__EXACT))
                {
                    listExpression.Add(o => o.CONTROL_CODE == this.CONTROL_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.MODULE_LINK__EXACT))
                {
                    listExpression.Add(o => o.MODULE_LINK == this.MODULE_LINK__EXACT);
                }

                if (this.PRINT_TYPE_ID.HasValue)
                {
                    listExpression.Add(o => o.PRINT_TYPE_ID == this.PRINT_TYPE_ID.Value);
                }

                if (this.PRINT_TYPE_IDs != null && this.PRINT_TYPE_IDs.Count > 0)
                {
                    listExpression.Add(o => this.PRINT_TYPE_IDs.Contains(o.PRINT_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.CONTROL_PATH__EXACT))
                {
                    listExpression.Add(o => o.CONTROL_PATH == this.CONTROL_PATH__EXACT);
                }

                search.listSarPrintTypeCfgExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<SAR_PRINT_TYPE_CFG>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSarPrintTypeCfgExpression.Clear();
                search.listSarPrintTypeCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
