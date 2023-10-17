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
    public class HisAllergenicFilterQuery : HisAllergenicFilter
    {
        public HisAllergenicFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ALLERGENIC, bool>>> listHisAllergenicExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ALLERGENIC, bool>>>();



        internal HisAllergenicSO Query()
        {
            HisAllergenicSO search = new HisAllergenicSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisAllergenicExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAllergenicExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAllergenicExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAllergenicExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisAllergenicExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ALLERGY_CARD_ID.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.ALLERGY_CARD_ID == this.ALLERGY_CARD_ID.Value);
                }
                if (this.ALLERGY_CARD_IDs != null)
                {
                    listHisAllergenicExpression.Add(o => this.ALLERGY_CARD_IDs.Contains(o.ALLERGY_CARD_ID));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listHisAllergenicExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listHisAllergenicExpression.Add(o => this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID));
                }
                if (this.IS_DOUBT.HasValue)
                {
                    if (this.IS_DOUBT.Value)
                    {
                        listHisAllergenicExpression.Add(o => o.IS_DOUBT.HasValue && o.IS_DOUBT.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisAllergenicExpression.Add(o => !o.IS_DOUBT.HasValue && o.IS_DOUBT.Value != Constant.IS_TRUE);
                    }
                }
                if (this.IS_SURE.HasValue)
                {
                    if (this.IS_DOUBT.Value)
                    {
                        listHisAllergenicExpression.Add(o => o.IS_SURE.HasValue && o.IS_SURE.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisAllergenicExpression.Add(o => !o.IS_SURE.HasValue && o.IS_SURE.Value != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_MEDICINE_TYPE_ID.HasValue)
                {
                    if (this.HAS_MEDICINE_TYPE_ID.Value)
                    {
                        listHisAllergenicExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listHisAllergenicExpression.Add(o => !o.MEDICINE_TYPE_ID.HasValue);
                    }
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisAllergenicExpression.Add(o => o.ALLERGENIC_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CLINICAL_EXPRESSION.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisAllergenicExpression.AddRange(listHisAllergenicExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAllergenicExpression.Clear();
                search.listHisAllergenicExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
