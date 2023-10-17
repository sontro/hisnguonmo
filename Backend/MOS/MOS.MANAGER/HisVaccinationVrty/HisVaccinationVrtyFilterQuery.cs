using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationVrty
{
    public class HisVaccinationVrtyFilterQuery : HisVaccinationVrtyFilter
    {
        public HisVaccinationVrtyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRTY, bool>>> listHisVaccinationVrtyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACCINATION_VRTY, bool>>>();

        

        internal HisVaccinationVrtySO Query()
        {
            HisVaccinationVrtySO search = new HisVaccinationVrtySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisVaccinationVrtyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVaccinationVrtyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVaccinationVrtyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVaccinationVrtyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisVaccinationVrtyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.VACC_REACT_TYPE_ID.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.VACC_REACT_TYPE_ID == this.VACC_REACT_TYPE_ID.Value);
                }
                if (this.VACC_REACT_TYPE_IDs != null)
                {
                    listHisVaccinationVrtyExpression.Add(o => this.VACC_REACT_TYPE_IDs.Contains(o.VACC_REACT_TYPE_ID));
                }
                if (this.VACCINATION_ID.HasValue)
                {
                    listHisVaccinationVrtyExpression.Add(o => o.VACCINATION_ID == this.VACCINATION_ID.Value);
                }
                if (this.VACCINATION_IDs != null)
                {
                    listHisVaccinationVrtyExpression.Add(o => this.VACCINATION_IDs.Contains(o.VACCINATION_ID));
                }

                search.listHisVaccinationVrtyExpression.AddRange(listHisVaccinationVrtyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVaccinationVrtyExpression.Clear();
                search.listHisVaccinationVrtyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
