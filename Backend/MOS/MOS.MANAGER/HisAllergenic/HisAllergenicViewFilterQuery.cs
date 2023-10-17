using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAllergenic
{
    public class HisAllergenicViewFilterQuery : HisAllergenicViewFilter
    {
        public HisAllergenicViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGENIC, bool>>> listVHisAllergenicExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ALLERGENIC, bool>>>();

        internal HisAllergenicSO Query()
        {
            HisAllergenicSO search = new HisAllergenicSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAllergenicExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAllergenicExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAllergenicExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAllergenicExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisAllergenicExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ALLERGY_CARD_ID.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.ALLERGY_CARD_ID == this.ALLERGY_CARD_ID.Value);
                }
                if (this.ALLERGY_CARD_IDs != null)
                {
                    listVHisAllergenicExpression.Add(o => this.ALLERGY_CARD_IDs.Contains(o.ALLERGY_CARD_ID));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listVHisAllergenicExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listVHisAllergenicExpression.Add(o => this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID));
                }
                if (this.IS_DOUBT.HasValue)
                {
                    if (this.IS_DOUBT.Value)
                    {
                        listVHisAllergenicExpression.Add(o => o.IS_DOUBT.HasValue && o.IS_DOUBT.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisAllergenicExpression.Add(o => !o.IS_DOUBT.HasValue && o.IS_DOUBT.Value != Constant.IS_TRUE);
                    }
                }
                if (this.IS_SURE.HasValue)
                {
                    if (this.IS_DOUBT.Value)
                    {
                        listVHisAllergenicExpression.Add(o => o.IS_SURE.HasValue && o.IS_SURE.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisAllergenicExpression.Add(o => !o.IS_SURE.HasValue && o.IS_SURE.Value != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_MEDICINE_TYPE_ID.HasValue)
                {
                    if (this.HAS_MEDICINE_TYPE_ID.Value)
                    {
                        listVHisAllergenicExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listVHisAllergenicExpression.Add(o => !o.MEDICINE_TYPE_ID.HasValue);
                    }
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAllergenicExpression.Add(o => 
                        o.ALLERGENIC_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CLINICAL_EXPRESSION.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisAllergenicExpression.AddRange(listVHisAllergenicExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAllergenicExpression.Clear();
                search.listVHisAllergenicExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
