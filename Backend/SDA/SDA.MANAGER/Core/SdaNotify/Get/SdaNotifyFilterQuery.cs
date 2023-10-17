using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaNotify.Get
{
    public class SdaNotifyFilterQuery : SdaNotifyFilter
    {
        public SdaNotifyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SDA_NOTIFY, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<SDA_NOTIFY, bool>>>();

        internal Inventec.Backend.MANAGER.OrderProcessorBase OrderProcess = new Inventec.Backend.MANAGER.OrderProcessorBase();

        internal SdaNotifySO Query()
        {
            SdaNotifySO search = new SdaNotifySO();
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

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o =>
                        o.CONTENT.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.RECEIVER_LOGINNAME_EXACT_OR_NULL))
                {
                    listExpression.Add(o => o.RECEIVER_LOGINNAME == this.RECEIVER_LOGINNAME_EXACT_OR_NULL || o.RECEIVER_LOGINNAME == null);
                }

                if (this.LOGIN_NAMES != null && this.LOGIN_NAMES.Count > 0)
                {
                    var searchPredicate = PredicateBuilder.False<SDA_NOTIFY>();

                    foreach (string str in this.LOGIN_NAMES)
                    {
                        var closureVariable = str;//can khai bao bien rieng de cho vao menh de ben duoi
                        searchPredicate = searchPredicate.Or(o => o.LOGIN_NAMES.Contains(closureVariable));
                    }

                    listExpression.Add(searchPredicate);
                }
                if (this.NOW_TIME.HasValue)
                {
                    listExpression.Add(o => o.FROM_TIME <= this.NOW_TIME.Value && (o.TO_TIME == null || o.TO_TIME >= this.NOW_TIME.Value));
                }
                else
                {
                    if (this.FROM_TIME.HasValue)
                    {
                        listExpression.Add(o => o.FROM_TIME >= this.FROM_TIME.Value);
                    }
                    if (this.TO_TIME.HasValue)
                    {
                        listExpression.Add(o => o.TO_TIME <= this.TO_TIME.Value);
                    }
                }

                if (this.HAS_RECEIVER_LOGINNAME_OR_NULL.HasValue)
                {
                    if (this.HAS_RECEIVER_LOGINNAME_OR_NULL.Value)
                    {
                        string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        listExpression.Add(o => o.RECEIVER_LOGINNAME == loginname || o.RECEIVER_LOGINNAME == null);
                    }
                    else
                    {
                        listExpression.Add(o => o.RECEIVER_LOGINNAME == null);
                    }
                }

                if (this.WATCHED.HasValue)
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    if (this.WATCHED.Value)
                    {
                        listExpression.Add(o => ("," + o.LOGIN_NAMES + ",").Contains("," + loginname + ","));
                    }
                    else
                    {
                        listExpression.Add(o => o.LOGIN_NAMES == null || !("," + o.LOGIN_NAMES + ",").Contains("," + loginname + ","));
                    }
                }
                if (!String.IsNullOrEmpty(this.RECEIVER_LOGINNAMES_EXACT_OR_NULL) )
                {
                    listExpression.Add(o => (o.RECEIVER_LOGINNAME == null && o.RECEIVER_LOGINNAMES == null)
                        || (o.RECEIVER_LOGINNAME == this.RECEIVER_LOGINNAMES_EXACT_OR_NULL)
                        || (("," + o.RECEIVER_LOGINNAMES + ",").Contains("," + this.RECEIVER_LOGINNAMES_EXACT_OR_NULL + ",")));
                }

                if (!String.IsNullOrEmpty(this.RECEIVER_DEPARTMENT_CODES_OR_NULL))
                {
                    listExpression.Add(o => o.RECEIVER_DEPARTMENT_CODES == null
                        || (("," + o.RECEIVER_DEPARTMENT_CODES + ",").Contains("," + this.RECEIVER_DEPARTMENT_CODES_OR_NULL + ",")));
                }

                search.listSdaNotifyExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<SDA_NOTIFY>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSdaNotifyExpression.Clear();
                search.listSdaNotifyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
