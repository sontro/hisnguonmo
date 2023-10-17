using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    public class HisMedicalAssessmentViewFilterQuery : HisMedicalAssessmentViewFilter
    {
        public HisMedicalAssessmentViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_ASSESSMENT, bool>>> listVHisMedicalAssessmentExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICAL_ASSESSMENT, bool>>>();

        

        internal HisMedicalAssessmentSO Query()
        {
            HisMedicalAssessmentSO search = new HisMedicalAssessmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMedicalAssessmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisMedicalAssessmentExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisMedicalAssessmentExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                search.listVHisMedicalAssessmentExpression.AddRange(listVHisMedicalAssessmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMedicalAssessmentExpression.Clear();
                search.listVHisMedicalAssessmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
