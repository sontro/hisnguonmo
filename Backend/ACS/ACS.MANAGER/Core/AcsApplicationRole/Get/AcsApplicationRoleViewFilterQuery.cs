using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get
{
    public class AcsApplicationRoleViewFilterQuery : AcsApplicationRoleViewFilter
    {
        public AcsApplicationRoleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_APPLICATION_ROLE, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal AcsApplicationRoleSO Query()
        {
            AcsApplicationRoleSO search = new AcsApplicationRoleSO();
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

                if (this.APPLICATION_ID.HasValue)
                {
                    listExpression.Add(o => o.APPLICATION_ID == this.APPLICATION_ID.Value);
                }
                if (this.ROLE_ID.HasValue)
                {
                    listExpression.Add(o => o.ROLE_ID == this.ROLE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.APPLICATION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.APPLICATION_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ROLE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ROLE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                #endregion

                search.listVAcsApplicationRoleExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<V_ACS_APPLICATION_ROLE>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVAcsApplicationRoleExpression.Clear();
                search.listVAcsApplicationRoleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
