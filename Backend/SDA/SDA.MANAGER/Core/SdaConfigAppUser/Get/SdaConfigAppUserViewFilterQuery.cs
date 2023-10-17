using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Get
{
    public class SdaConfigAppUserViewFilterQuery : SdaConfigAppUserViewFilter
    {
        public SdaConfigAppUserViewFilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_SDA_CONFIG_APP_USER, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_CONFIG_APP_USER, bool>>>();

        internal Inventec.Backend.MANAGER.OrderProcessorBase OrderProcess = new Inventec.Backend.MANAGER.OrderProcessorBase();

        internal SdaConfigAppUserSO Query()
        {
            SdaConfigAppUserSO search = new SdaConfigAppUserSO();
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
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.VALUE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEY__EXACT))
                {
                    listExpression.Add(o => o.KEY == this.KEY__EXACT);
                }

                if (!String.IsNullOrEmpty(this.LOGINNAME_EXACT))
                {
                    listExpression.Add(o => o.LOGINNAME == this.LOGINNAME_EXACT);
                }
                else
                {
                    string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    listExpression.Add(o => o.LOGINNAME == loginname);
                }

                search.listVSdaConfigAppUserExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<V_SDA_CONFIG_APP_USER>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVSdaConfigAppUserExpression.Clear();
                search.listVSdaConfigAppUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
