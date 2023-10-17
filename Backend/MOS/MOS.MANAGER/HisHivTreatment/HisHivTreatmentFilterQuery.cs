using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    public class HisHivTreatmentFilterQuery : HisHivTreatmentFilter
    {
        public HisHivTreatmentFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HIV_TREATMENT, bool>>> listHisHivTreatmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HIV_TREATMENT, bool>>>();

        

        internal HisHivTreatmentSO Query()
        {
            HisHivTreatmentSO search = new HisHivTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisHivTreatmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHivTreatmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHivTreatmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHivTreatmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IS_DELETE.HasValue)
                {
                    listHisHivTreatmentExpression.Add(o => o.IS_DELETE == this.IS_DELETE.Value);
                }

                if (this.TREATMENT_ID != null)
                {
                    listHisHivTreatmentExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }

                if (this.TREATMENT_IDs != null)
                {
                    listHisHivTreatmentExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisHivTreatmentExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.REGIMEN_HIV_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listHisHivTreatmentExpression.AddRange(listHisHivTreatmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHivTreatmentExpression.Clear();
                search.listHisHivTreatmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
