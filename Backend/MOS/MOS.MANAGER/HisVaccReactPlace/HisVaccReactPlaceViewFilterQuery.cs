using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    public class HisVaccReactPlaceViewFilterQuery : HisVaccReactPlaceViewFilter
    {
        public HisVaccReactPlaceViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_REACT_PLACE, bool>>> listVHisVaccReactPlaceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACC_REACT_PLACE, bool>>>();

        

        internal HisVaccReactPlaceSO Query()
        {
            HisVaccReactPlaceSO search = new HisVaccReactPlaceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisVaccReactPlaceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccReactPlaceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVaccReactPlaceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisVaccReactPlaceExpression.AddRange(listVHisVaccReactPlaceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccReactPlaceExpression.Clear();
                search.listVHisVaccReactPlaceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
