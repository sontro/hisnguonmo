using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrpl
{
    public class HisVaccinationVrplFilterQuery : HisVaccinationVrplFilter
    {
        public HisVaccinationVrplFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRPL, bool>>> listHisVaccinationVrplExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRPL, bool>>>();

        

        internal HisVaccinationVrplSO Query()
        {
            HisVaccinationVrplSO search = new HisVaccinationVrplSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisVaccinationVrplExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVaccinationVrplExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVaccinationVrplExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVaccinationVrplExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisVaccinationVrplExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.VACC_REACT_PLACE_ID.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.VACC_REACT_PLACE_ID == this.VACC_REACT_PLACE_ID.Value);
                }
                if (this.VACC_REACT_PLACE_IDs != null)
                {
                    listHisVaccinationVrplExpression.Add(o => this.VACC_REACT_PLACE_IDs.Contains(o.VACC_REACT_PLACE_ID));
                }
                if (this.VACCINATION_ID.HasValue)
                {
                    listHisVaccinationVrplExpression.Add(o => o.VACCINATION_ID == this.VACCINATION_ID.Value);
                }
                if (this.VACCINATION_IDs != null)
                {
                    listHisVaccinationVrplExpression.Add(o => this.VACCINATION_IDs.Contains(o.VACCINATION_ID));
                }

                search.listHisVaccinationVrplExpression.AddRange(listHisVaccinationVrplExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVaccinationVrplExpression.Clear();
                search.listHisVaccinationVrplExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
