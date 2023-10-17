using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationReact
{
    public class HisVaccinationReactViewFilterQuery : HisVaccinationReactViewFilter
    {
        public HisVaccinationReactViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_REACT, bool>>> listVHisVaccinationReactExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_REACT, bool>>>();

        

        internal HisVaccinationReactSO Query()
        {
            HisVaccinationReactSO search = new HisVaccinationReactSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisVaccinationReactExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccinationReactExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccinationReactExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccinationReactExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccinationReactExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVaccinationReactExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisVaccinationReactExpression.AddRange(listVHisVaccinationReactExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccinationReactExpression.Clear();
                search.listVHisVaccinationReactExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
