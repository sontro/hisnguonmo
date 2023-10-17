using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaDeleteData.Get
{
    public class SdaDeleteDataFilterQuery : SdaDeleteDataFilter
    {
        public SdaDeleteDataFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SDA_DELETE_DATA, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<SDA_DELETE_DATA, bool>>>();

        internal Inventec.Backend.MANAGER.OrderProcessorBase OrderProcess = new Inventec.Backend.MANAGER.OrderProcessorBase();

        internal SdaDeleteDataSO Query()
        {
            SdaDeleteDataSO search = new SdaDeleteDataSO();
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
                if (this.DATA_ID.HasValue)
                {
                    listExpression.Add(o => o.DATA_ID == this.DATA_ID.Value);
                }
                if (this.DATA_IDs != null && this.DATA_IDs.Count > 0)
                {
                    listExpression.Add(o => this.DATA_IDs.Contains(o.DATA_ID));
                }

                search.listSdaDeleteDataExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<SDA_DELETE_DATA>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSdaDeleteDataExpression.Clear();
                search.listSdaDeleteDataExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
