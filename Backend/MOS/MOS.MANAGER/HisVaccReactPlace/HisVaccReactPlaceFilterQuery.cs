using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccReactPlace
{
    public class HisVaccReactPlaceFilterQuery : HisVaccReactPlaceFilter
    {
        public HisVaccReactPlaceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_PLACE, bool>>> listHisVaccReactPlaceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_REACT_PLACE, bool>>>();

        

        internal HisVaccReactPlaceSO Query()
        {
            HisVaccReactPlaceSO search = new HisVaccReactPlaceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisVaccReactPlaceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVaccReactPlaceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVaccReactPlaceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVaccReactPlaceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVaccReactPlaceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisVaccReactPlaceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisVaccReactPlaceExpression.AddRange(listHisVaccReactPlaceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVaccReactPlaceExpression.Clear();
                search.listHisVaccReactPlaceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
