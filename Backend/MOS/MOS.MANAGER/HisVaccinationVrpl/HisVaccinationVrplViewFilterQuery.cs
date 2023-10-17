using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    public class HisVaccinationVrplViewFilterQuery : HisVaccinationVrplViewFilter
    {
        public HisVaccinationVrplViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_VRPL, bool>>> listVHisVaccinationVrplExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_VRPL, bool>>>();

        

        internal HisVaccinationVrplSO Query()
        {
            HisVaccinationVrplSO search = new HisVaccinationVrplSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisVaccinationVrplExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccinationVrplExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccinationVrplExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccinationVrplExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccinationVrplExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVaccinationVrplExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisVaccinationVrplExpression.AddRange(listVHisVaccinationVrplExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccinationVrplExpression.Clear();
                search.listVHisVaccinationVrplExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
