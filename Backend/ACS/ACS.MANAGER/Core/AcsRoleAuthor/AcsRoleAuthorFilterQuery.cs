using Inventec.Common.Logging;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsRoleAuthor
{
    public class AcsRoleAuthorFilterQuery : AcsRoleAuthorFilter
    {
        public AcsRoleAuthorFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_ROLE_AUTHOR, bool>>> listAcsRoleAuthorExpression = new List<System.Linq.Expressions.Expression<Func<ACS_ROLE_AUTHOR, bool>>>();

        

        internal AcsRoleAuthorSO Query()
        {
            AcsRoleAuthorSO search = new AcsRoleAuthorSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listAcsRoleAuthorExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listAcsRoleAuthorExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listAcsRoleAuthorExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listAcsRoleAuthorExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }

                if (this.ROLE_ID.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.ROLE_ID == this.ROLE_ID.Value);
                }
                if (this.AUTHOR_SYSTEM_ID.HasValue)
                {
                    listAcsRoleAuthorExpression.Add(o => o.AUTHOR_SYSTEM_ID == this.AUTHOR_SYSTEM_ID.Value);
                }
                #endregion
                
                search.listAcsRoleAuthorExpression.AddRange(listAcsRoleAuthorExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsRoleAuthorExpression.Clear();
                search.listAcsRoleAuthorExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
