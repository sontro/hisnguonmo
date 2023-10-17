using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateTemp
{
    public class HisDebateTempFilterQuery : HisDebateTempFilter
    {
        public HisDebateTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_TEMP, bool>>> listHisDebateTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_TEMP, bool>>>();



        internal HisDebateTempSO Query()
        {
            HisDebateTempSO search = new HisDebateTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisDebateTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDebateTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDebateTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDebateTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDebateTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisDebateTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.DEBATE_CODE__EXACT))
                {
                    listHisDebateTempExpression.Add(o => o.DEBATE_TEMP_CODE == this.DEBATE_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD__CODE_OR_NAME))
                {
                    this.KEY_WORD__CODE_OR_NAME = this.KEY_WORD__CODE_OR_NAME.ToLower().Trim();
                    listHisDebateTempExpression.Add(o => o.DEBATE_TEMP_CODE.ToLower().Contains(this.KEY_WORD__CODE_OR_NAME)
                        || o.DEBATE_TEMP_NAME.ToLower().Contains(this.KEY_WORD__CODE_OR_NAME)
                        );
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDebateTempExpression.Add(o => o.BEFORE_DIAGNOSTIC.ToLower().Contains(this.KEY_WORD)
                        || o.CARE_METHOD.ToLower().Contains(this.KEY_WORD)
                        || o.CONCLUSION.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DEBATE_TEMP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.DEBATE_TEMP_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.DIAGNOSTIC.ToLower().Contains(this.KEY_WORD)
                        || o.DISCUSSION.ToLower().Contains(this.KEY_WORD)
                        || o.HOSPITALIZATION_STATE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_SUB_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_TEXT.ToLower().Contains(this.KEY_WORD)
                        || o.LOCATION.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_CONCENTRA.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_TUTORIAL.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_USE_FORM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PATHOLOGICAL_HISTORY.ToLower().Contains(this.KEY_WORD)
                        || o.REQUEST_CONTENT.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_METHOD.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_TRACKING.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisDebateTempExpression.AddRange(listHisDebateTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDebateTempExpression.Clear();
                search.listHisDebateTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
