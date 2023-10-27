using Inventec.Common.Logging;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthorSystem
{
    public class AcsAuthorSystemViewFilterQuery : AcsAuthorSystemViewFilter
    {
        public AcsAuthorSystemViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_ACS_AUTHOR_SYSTEM, bool>>> listVAcsAuthorSystemExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_AUTHOR_SYSTEM, bool>>>();

        

        internal AcsAuthorSystemSO Query()
        {
            AcsAuthorSystemSO search = new AcsAuthorSystemSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVAcsAuthorSystemExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVAcsAuthorSystemExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVAcsAuthorSystemExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVAcsAuthorSystemExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVAcsAuthorSystemExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVAcsAuthorSystemExpression.AddRange(listVAcsAuthorSystemExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVAcsAuthorSystemExpression.Clear();
                search.listVAcsAuthorSystemExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
