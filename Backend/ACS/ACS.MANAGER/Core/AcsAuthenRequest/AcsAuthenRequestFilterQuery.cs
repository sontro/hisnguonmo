using Inventec.Common.Logging;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsAuthenRequest
{
    public class AcsAuthenRequestFilterQuery : AcsAuthenRequestFilter
    {
        public AcsAuthenRequestFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_AUTHEN_REQUEST, bool>>> listAcsAuthenRequestExpression = new List<System.Linq.Expressions.Expression<Func<ACS_AUTHEN_REQUEST, bool>>>();



        internal AcsAuthenRequestSO Query()
        {
            AcsAuthenRequestSO search = new AcsAuthenRequestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listAcsAuthenRequestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listAcsAuthenRequestExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listAcsAuthenRequestExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listAcsAuthenRequestExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listAcsAuthenRequestExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }

                if (!String.IsNullOrEmpty(this.AUTHENTICATION_CODE))
                {
                    listAcsAuthenRequestExpression.Add(o => o.AUTHENTICATION_CODE == this.AUTHENTICATION_CODE);
                }
                if (!String.IsNullOrEmpty(this.AUTHOR_SYSTEM_CODE))
                {
                    listAcsAuthenRequestExpression.Add(o => o.TDL_AUTHOR_SYSTEM_CODE == this.AUTHOR_SYSTEM_CODE);
                }
                if (this.IS_ISSUED_TOKEN.HasValue)
                {
                    if (this.IS_ISSUED_TOKEN.Value)
                    {
                        listAcsAuthenRequestExpression.Add(o => o.IS_ISSUED_TOKEN == 1);
                    }
                    else
                        listAcsAuthenRequestExpression.Add(o => o.IS_ISSUED_TOKEN == null || o.IS_ISSUED_TOKEN != 1);
                }
                #endregion

                search.listAcsAuthenRequestExpression.AddRange(listAcsAuthenRequestExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsAuthenRequestExpression.Clear();
                search.listAcsAuthenRequestExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
