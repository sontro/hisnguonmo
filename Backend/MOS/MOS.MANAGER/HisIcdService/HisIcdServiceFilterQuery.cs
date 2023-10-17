using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdService
{
    public class HisIcdServiceFilterQuery : HisIcdServiceFilter
    {
        public HisIcdServiceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ICD_SERVICE, bool>>> listHisIcdServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ICD_SERVICE, bool>>>();



        internal HisIcdServiceSO Query()
        {
            HisIcdServiceSO search = new HisIcdServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisIcdServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisIcdServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisIcdServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisIcdServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisIcdServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.SERVICE_ID.HasValue && o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisIcdServiceExpression.Add(o => o.SERVICE_ID.HasValue && this.SERVICE_IDs.Contains(o.SERVICE_ID.Value));
                }
                if (this.ACTIVE_INGREDIENT_ID.HasValue)
                {
                    listHisIcdServiceExpression.Add(o => o.ACTIVE_INGREDIENT_ID.HasValue && o.ACTIVE_INGREDIENT_ID == this.ACTIVE_INGREDIENT_ID.Value);
                }
                if (this.ACTIVE_INGREDIENT_IDs != null)
                {
                    listHisIcdServiceExpression.Add(o => o.ACTIVE_INGREDIENT_ID.HasValue && this.ACTIVE_INGREDIENT_IDs.Contains(o.ACTIVE_INGREDIENT_ID.Value));
                }
                if (!String.IsNullOrEmpty(this.ICD_CODE__EXACT))
                {
                    listHisIcdServiceExpression.Add(o => o.ICD_CODE == this.ICD_CODE__EXACT);
                }
                if (this.ICD_CODE__EXACTs != null)
                {
                    listHisIcdServiceExpression.Add(o => this.ICD_CODE__EXACTs.Contains(o.ICD_CODE));
                }
                if (this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID != null)
                {
                    if (this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ServiceIds == null)
                    {
                        this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ServiceIds = new List<long>();
                    }
                    if (this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ActiveIngredientIds == null)
                    {
                        this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ActiveIngredientIds = new List<long>();
                    }

                    listHisIcdServiceExpression.Add(o => (o.SERVICE_ID.HasValue && this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ServiceIds.Contains(o.SERVICE_ID.Value))
                            || (o.ACTIVE_INGREDIENT_ID.HasValue && this.SERVICE_ID_OR_ACTIVE_INGREDIENT_ID.ActiveIngredientIds.Contains(o.ACTIVE_INGREDIENT_ID.Value)));
                }

                search.listHisIcdServiceExpression.AddRange(listHisIcdServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisIcdServiceExpression.Clear();
                search.listHisIcdServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
