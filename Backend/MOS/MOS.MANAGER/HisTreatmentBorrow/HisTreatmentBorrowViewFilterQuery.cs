using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    public class HisTreatmentBorrowViewFilterQuery : HisTreatmentBorrowViewFilter
    {
        public HisTreatmentBorrowViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BORROW, bool>>> listVHisTreatmentBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BORROW, bool>>>();

        internal HisTreatmentBorrowSO Query()
        {
            HisTreatmentBorrowSO search = new HisTreatmentBorrowSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.DATA_STORE_ID.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.DATA_STORE_ID.HasValue && o.DATA_STORE_ID.Value == this.DATA_STORE_ID.Value);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }

                if (this.GIVE_DATE_FROM.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.GIVE_DATE >= this.GIVE_DATE_FROM.Value);
                }
                if (this.GIVE_DATE_TO.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.GIVE_DATE <= this.GIVE_DATE_TO.Value);
                }
                if (this.RECEIVE_DATE_FROM.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.RECEIVE_DATE.HasValue && o.RECEIVE_DATE.Value >= this.RECEIVE_DATE_FROM.Value);
                }
                if (this.RECEIVE_DATE_TO.HasValue)
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.RECEIVE_DATE.HasValue && o.RECEIVE_DATE.Value <= this.RECEIVE_DATE_TO.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.BORROW_LOGINNAME__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.BORROW_LOGINNAME == this.BORROW_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.GIVER_LOGINNAME__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.GIVER_LOGINNAME == this.GIVER_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.RECEIVER_LOGINNAME__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.RECEIVER_LOGINNAME == this.RECEIVER_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.DEPARTMENT_CODE__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.DEPARTMENT_CODE == this.DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.STORE_CODE__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.STORE_CODE == this.STORE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.DATA_STORE_CODE__EXACT))
                {
                    listVHisTreatmentBorrowExpression.Add(o => o.DATA_STORE_CODE == this.DATA_STORE_CODE__EXACT);
                }
                if (this.IS_RECEIVE.HasValue)
                {
                    if (this.IS_RECEIVE.Value)
                    {
                        listVHisTreatmentBorrowExpression.Add(o => o.RECEIVE_TIME.HasValue);
                    }
                    else
                    {
                        listVHisTreatmentBorrowExpression.Add(o => !o.RECEIVE_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisTreatmentBorrowExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisTreatmentBorrowExpression.AddRange(listVHisTreatmentBorrowExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentBorrowExpression.Clear();
                search.listVHisTreatmentBorrowExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
