using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    public class HisMedicalAssessmentFilterQuery : HisMedicalAssessmentFilter
    {
        public HisMedicalAssessmentFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_ASSESSMENT, bool>>> listHisMedicalAssessmentExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_ASSESSMENT, bool>>>();

        

        internal HisMedicalAssessmentSO Query()
        {
            HisMedicalAssessmentSO search = new HisMedicalAssessmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMedicalAssessmentExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicalAssessmentExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicalAssessmentExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicalAssessmentExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.IS_DELETE.HasValue)
                {
                    listHisMedicalAssessmentExpression.Add(o => o.IS_DELETE == this.IS_DELETE.Value);
                }

                search.listHisMedicalAssessmentExpression.AddRange(listHisMedicalAssessmentExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicalAssessmentExpression.Clear();
                search.listHisMedicalAssessmentExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
