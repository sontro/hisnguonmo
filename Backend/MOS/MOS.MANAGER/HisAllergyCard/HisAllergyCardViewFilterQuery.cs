using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergyCard
{
    public class HisAllergyCardViewFilterQuery : HisAllergyCardViewFilter
    {
        public HisAllergyCardViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGY_CARD, bool>>> listVHisAllergyCardExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGY_CARD, bool>>>();

        

        internal HisAllergyCardSO Query()
        {
            HisAllergyCardSO search = new HisAllergyCardSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAllergyCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAllergyCardExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAllergyCardExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAllergyCardExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisAllergyCardExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisAllergyCardExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.ISSUE_TIME_FROM.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.ISSUE_TIME >= this.ISSUE_TIME_FROM.Value);
                }
                if (this.ISSUE_TIME_TO.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.ISSUE_TIME <= this.ISSUE_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisAllergyCardExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisAllergyCardExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.DIAGNOSE_LOGINNAME__EXACT))
                {
                    listVHisAllergyCardExpression.Add(o => o.DIAGNOSE_LOGINNAME == this.DIAGNOSE_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisAllergyCardExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisAllergyCardExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAllergyCardExpression.Add(o => o.CCCD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CMND_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)||
                        o.DIAGNOSE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DIAGNOSE_PHONE.ToLower().Contains(this.KEY_WORD) ||
                        o.DIAGNOSE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisAllergyCardExpression.AddRange(listVHisAllergyCardExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAllergyCardExpression.Clear();
                search.listVHisAllergyCardExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
