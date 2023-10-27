using Inventec.Common.Logging;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthorSystem
{
    public class AcsAuthorSystemFilterQuery : AcsAuthorSystemFilter
    {
        public AcsAuthorSystemFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_AUTHOR_SYSTEM, bool>>> listAcsAuthorSystemExpression = new List<System.Linq.Expressions.Expression<Func<ACS_AUTHOR_SYSTEM, bool>>>();

        

        internal AcsAuthorSystemSO Query()
        {
            AcsAuthorSystemSO search = new AcsAuthorSystemSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listAcsAuthorSystemExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listAcsAuthorSystemExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listAcsAuthorSystemExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listAcsAuthorSystemExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listAcsAuthorSystemExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }

                if (!String.IsNullOrEmpty(this.AUTHOR_SYSTEM_CODE))
                {
                    listAcsAuthorSystemExpression.Add(o => o.AUTHOR_SYSTEM_CODE == this.AUTHOR_SYSTEM_CODE);
                }
                #endregion
                
                search.listAcsAuthorSystemExpression.AddRange(listAcsAuthorSystemExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsAuthorSystemExpression.Clear();
                search.listAcsAuthorSystemExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
