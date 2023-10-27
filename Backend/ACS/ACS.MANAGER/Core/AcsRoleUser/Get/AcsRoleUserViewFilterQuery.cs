using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Get
{
    public class AcsRoleUserViewFilterQuery : AcsRoleUserViewFilter
    {
        public AcsRoleUserViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_USER, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_ROLE_USER, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal AcsRoleUserSO Query()
        {
            AcsRoleUserSO search = new AcsRoleUserSO();
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

                if (this.USER_ID.HasValue)
                {
                    listExpression.Add(o => o.USER_ID == this.USER_ID.Value);
                }
                if (this.ROLE_ID.HasValue)
                {
                    listExpression.Add(o => o.ROLE_ID == this.ROLE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.LOGINNAME))
                {
                    listExpression.Add(o => o.LOGINNAME == this.LOGINNAME);
                }
                if (!String.IsNullOrEmpty(this.ROLE_CODE))
                {
                    listExpression.Add(o => o.ROLE_CODE == this.ROLE_CODE);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o => o.LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.ROLE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ROLE_NAME.ToLower().Contains(this.KEY_WORD));
                }
                #endregion

                search.listVAcsRoleUserExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<V_ACS_ROLE_USER>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVAcsRoleUserExpression.Clear();
                search.listVAcsRoleUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
