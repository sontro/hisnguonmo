using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    public class HisHivTreatmentViewFilterQuery : HisHivTreatmentViewFilter
    {
        public HisHivTreatmentViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_HIV_TREATMENT, bool>>> listVHisHivTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HIV_TREATMENT, bool>>>();

        

        internal HisHivTreatmentSO Query()
        {
            HisHivTreatmentSO search = new HisHivTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisHivTreatmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisHivTreatmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisHivTreatmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisHivTreatmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }

                if (this.IN_TIME_FROM.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.IN_TIME >= this.IN_TIME_FROM.Value);
                }
                if (this.IN_TIME_TO.HasValue)
                {
                    listVHisHivTreatmentExpression.Add(o => o.IN_TIME <= this.IN_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisHivTreatmentExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.REGIMEN_HIV_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                search.listVHisHivTreatmentExpression.AddRange(listVHisHivTreatmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisHivTreatmentExpression.Clear();
                search.listVHisHivTreatmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
