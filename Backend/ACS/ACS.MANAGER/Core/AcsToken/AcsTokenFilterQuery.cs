using Inventec.Common.Logging;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsToken
{
    public class AcsTokenFilterQuery : AcsTokenFilter
    {
        public AcsTokenFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_TOKEN, bool>>> listAcsTokenExpression = new List<System.Linq.Expressions.Expression<Func<ACS_TOKEN, bool>>>();



        internal AcsTokenSO Query()
        {
            AcsTokenSO search = new AcsTokenSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listAcsTokenExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listAcsTokenExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listAcsTokenExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listAcsTokenExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listAcsTokenExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listAcsTokenExpression.Add(o => this.IDs.Contains(o.ID));
                }

                if (!String.IsNullOrEmpty(this.TokenCode))
                {
                    listAcsTokenExpression.Add(o => o.TOKEN_CODE == this.TokenCode);
                }
                if (this.IDs != null)
                {
                    listAcsTokenExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (!String.IsNullOrEmpty(this.RenewCode))
                {
                    listAcsTokenExpression.Add(o => o.RENEW_CODE == this.RenewCode);
                }
                if (!String.IsNullOrEmpty(this.ApplicationCode))
                {
                    listAcsTokenExpression.Add(o => o.APPLICATION_CODE == this.ApplicationCode);
                }
                if (!String.IsNullOrEmpty(this.MachineName))
                {
                    listAcsTokenExpression.Add(o => o.MACHINE_NAME == this.MachineName);
                }
                if (!String.IsNullOrEmpty(this.LoginName))
                {
                    listAcsTokenExpression.Add(o => o.LOGIN_NAME.ToLower() == this.LoginName.ToLower());
                }
                if (this.LoginNames != null && this.LoginNames.Count > 0)
                {
                    listAcsTokenExpression.Add(o => this.LoginNames.Exists(k => k == o.LOGIN_NAME));
                }
                #endregion

                search.listAcsTokenExpression.AddRange(listAcsTokenExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsTokenExpression.Clear();
                search.listAcsTokenExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
