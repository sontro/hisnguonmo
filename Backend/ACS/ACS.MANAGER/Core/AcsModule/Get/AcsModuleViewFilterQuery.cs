using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule.Get
{
    public class AcsModuleViewFilterQuery : AcsModuleViewFilter
    {
        public AcsModuleViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<V_ACS_MODULE, bool>>>();

        internal Inventec.Backend.MANAGER.OrderProcessorBase OrderProcess = new Inventec.Backend.MANAGER.OrderProcessorBase();

        internal AcsModuleSO Query()
        {
            AcsModuleSO search = new AcsModuleSO();
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
                if (this.APPLICATION_ID.HasValue)
                {
                    listExpression.Add(o => o.APPLICATION_ID == this.APPLICATION_ID.Value);
                }
                if (this.IsLeaf.HasValue)
                {
                    if (this.IsLeaf.Value)
                    {
                        listExpression.Add(o => !String.IsNullOrEmpty(o.MODULE_LINK));
                    }
                    else
                    {
                        listExpression.Add(o => String.IsNullOrEmpty(o.MODULE_LINK));
                    }
                }
                if (this.IsParent.HasValue)
                {
                    if (this.IsParent.Value)
                    {
                        listExpression.Add(o => (o.PARENT_ID ?? 0) == 0);
                    }
                    else
                    {
                        listExpression.Add(o => (o.PARENT_ID ?? 0) > 0);
                    }
                }
                if (this.IsAnonymous.HasValue)
                {
                    if (this.IsAnonymous.Value)
                    {
                        listExpression.Add(o => o.IS_ANONYMOUS == 1);
                    }
                    else
                    {
                        listExpression.Add(o => (o.IS_ANONYMOUS ?? 0) != 1);
                    }
                }
                if (this.IS_LEAF.HasValue)
                {
                    listExpression.Add(o => o.IS_LEAF == this.IS_LEAF.Value);
                }
                if (this.IS_VISIBLE.HasValue)
                {
                    listExpression.Add(o => o.IS_VISIBLE == this.IS_VISIBLE.Value);
                }
                if (!String.IsNullOrEmpty(this.APPLICATION_CODE))
                {
                    listExpression.Add(o => o.APPLICATION_CODE == this.APPLICATION_CODE);
                }
                if (!String.IsNullOrEmpty(this.MODULE_LINK))
                {
                    listExpression.Add(o => o.MODULE_LINK == this.MODULE_LINK);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_LINK.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_LINK.ToLower().Contains(this.KEY_WORD) ||
                        o.ICON_LINK.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_GROUP_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PARENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPLICATION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.APPLICATION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.IsTree.HasValue && this.IsTree.Value)
                {
                    listExpression.Add(o => (!o.PARENT_ID.HasValue && !this.Node.HasValue) || (o.PARENT_ID.HasValue && o.PARENT_ID.Value == this.Node.Value));
                }

                search.listVAcsModuleExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<V_ACS_MODULE>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVAcsModuleExpression.Clear();
                search.listVAcsModuleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
