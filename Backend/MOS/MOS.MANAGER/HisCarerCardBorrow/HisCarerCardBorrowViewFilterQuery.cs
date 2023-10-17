using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    public class HisCarerCardBorrowViewFilterQuery : HisCarerCardBorrowViewFilter
    {
        public HisCarerCardBorrowViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD_BORROW, bool>>> listVHisCarerCardBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARER_CARD_BORROW, bool>>>();

        

        internal HisCarerCardBorrowSO Query()
        {
            HisCarerCardBorrowSO search = new HisCarerCardBorrowSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.CARER_CARD_ID.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.CARER_CARD_ID == this.CARER_CARD_ID.Value);
                }
                if (this.CARER_CARD_IDs != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => this.CARER_CARD_IDs.Contains(o.CARER_CARD_ID));
                }

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.CARER_CARD_NUMBER__EXACT != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.CARER_CARD_NUMBER == this.CARER_CARD_NUMBER__EXACT);
                }
                if (this.TREATMENT_CODE__EXACT != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.IN_CODE__EXACT != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.IN_CODE == this.IN_CODE__EXACT);
                }
                if (this.PATIENT_CODE__EXACT != null)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TDL_PATIENT_NAME))
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.TDL_PATIENT_NAME.Trim().ToLower()));
                }
                if (this.BORROW_TIME__FROM.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.BORROW_TIME >= this.BORROW_TIME__FROM.Value);
                }
                if (this.BORROW_TIME__TO.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.BORROW_TIME <= this.BORROW_TIME__TO.Value);
                }
                if (this.GIVE_BACK_TIME__FROM.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.GIVE_BACK_TIME.Value >= this.GIVE_BACK_TIME__FROM.Value);
                }
                if (this.GIVE_BACK_TIME__TO.HasValue)
                {
                    listVHisCarerCardBorrowExpression.Add(o => o.GIVE_BACK_TIME.Value <= this.GIVE_BACK_TIME__TO.Value);
                }
                if (this.HAS_GIVE_BACK_TIME.HasValue)
                {
                    if (this.HAS_GIVE_BACK_TIME.Value)
                    {
                        listVHisCarerCardBorrowExpression.Add(o => o.GIVE_BACK_TIME.HasValue);
                    }
                    else
                    {
                        listVHisCarerCardBorrowExpression.Add(o => !o.GIVE_BACK_TIME.HasValue);
                    }
                }
                if (this.IS_LOST.HasValue)
                {
                    if (this.IS_LOST.Value)
                    {
                        listVHisCarerCardBorrowExpression.Add(o => o.IS_LOST.HasValue && o.IS_LOST == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisCarerCardBorrowExpression.Add(o => !o.IS_LOST.HasValue || o.IS_LOST != Constant.IS_TRUE);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisCarerCardBorrowExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GIVING_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.GIVING_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_CLASSIFY_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CAREER_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CCCD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CMND_NUMBER.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_WORK_PLACE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_WORK_PLACE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.IN_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.CARER_CARD_NUMBER.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisCarerCardBorrowExpression.AddRange(listVHisCarerCardBorrowExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisCarerCardBorrowExpression.Clear();
                search.listVHisCarerCardBorrowExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
