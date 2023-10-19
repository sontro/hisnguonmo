using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsCredentialData.Get
{
    public class AcsCredentialDataFilterQuery : AcsCredentialDataFilter
    {
        public AcsCredentialDataFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_CREDENTIAL_DATA, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<ACS_CREDENTIAL_DATA, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal AcsCredentialDataSO Query()
        {
            AcsCredentialDataSO search = new AcsCredentialDataSO();
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
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
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

                if (!String.IsNullOrEmpty(this.TOKEN_CODE))
                {
                    listExpression.Add(o => o.TOKEN_CODE == this.TOKEN_CODE);
                }
                if (!String.IsNullOrEmpty(this.RESOURCE_SYSTEM_CODE))
                {
                    listExpression.Add(o => o.RESOURCE_SYSTEM_CODE == this.RESOURCE_SYSTEM_CODE);
                }
                if (!String.IsNullOrEmpty(this.DATA_KEY))
                {
                    listExpression.Add(o => o.DATA_KEY == this.DATA_KEY);
                }
                if (this.TOKEN_CODEs != null && this.TOKEN_CODEs.Count > 0)
                {
                    listExpression.Add(o => this.TOKEN_CODEs.Contains(o.TOKEN_CODE));
                }
                if (this.NOT_IN_TOKEN_CODEs != null && this.NOT_IN_TOKEN_CODEs.Count > 0)
                {
                    listExpression.Add(o => !this.NOT_IN_TOKEN_CODEs.Contains(o.TOKEN_CODE));
                }
                #endregion

                search.listAcsCredentialDataExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<ACS_CREDENTIAL_DATA>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsCredentialDataExpression.Clear();
                search.listAcsCredentialDataExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
