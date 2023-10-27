using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModule.Get
{
    public class AcsModuleFilterQuery : AcsModuleFilter
    {
        public AcsModuleFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<ACS_MODULE, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<ACS_MODULE, bool>>>();

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
                #endregion
                if (this.APPLICATION_ID.HasValue)
                {
                    listExpression.Add(o => o.APPLICATION_ID == this.APPLICATION_ID.Value);
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
                        o.ICON_LINK.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listAcsModuleExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<ACS_MODULE>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listAcsModuleExpression.Clear();
                search.listAcsModuleExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
