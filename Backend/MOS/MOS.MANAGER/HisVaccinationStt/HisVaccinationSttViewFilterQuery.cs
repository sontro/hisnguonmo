using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    public class HisVaccinationSttViewFilterQuery : HisVaccinationSttViewFilter
    {
        public HisVaccinationSttViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_STT, bool>>> listVHisVaccinationSttExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_STT, bool>>>();

        

        internal HisVaccinationSttSO Query()
        {
            HisVaccinationSttSO search = new HisVaccinationSttSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisVaccinationSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccinationSttExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccinationSttExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccinationSttExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccinationSttExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVaccinationSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisVaccinationSttExpression.AddRange(listVHisVaccinationSttExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccinationSttExpression.Clear();
                search.listVHisVaccinationSttExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
