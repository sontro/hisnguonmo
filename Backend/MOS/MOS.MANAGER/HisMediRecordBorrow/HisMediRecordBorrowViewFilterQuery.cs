using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    public class HisMediRecordBorrowViewFilterQuery : HisMediRecordBorrowViewFilter
    {
        public HisMediRecordBorrowViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_BORROW, bool>>> listVHisMediRecordBorrowExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_BORROW, bool>>>();



        internal HisMediRecordBorrowSO Query()
        {
            HisMediRecordBorrowSO search = new HisMediRecordBorrowSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMediRecordBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMediRecordBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.MEDI_RECORD_ID.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.MEDI_RECORD_ID == this.MEDI_RECORD_ID.Value);
                }
                if (this.MEDI_RECORD_IDs != null)
                {
                    listVHisMediRecordBorrowExpression.Add(o => this.MEDI_RECORD_IDs.Contains(o.MEDI_RECORD_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisMediRecordBorrowExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.DATA_STORE_ID.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.DATA_STORE_ID.HasValue && o.DATA_STORE_ID.Value == this.DATA_STORE_ID.Value);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.GIVE_DATE__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.GIVE_DATE.HasValue && o.GIVE_DATE.Value == this.GIVE_DATE__EQUAL.Value);
                }
                if (this.GIVE_MONTH__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.VIR_GIVE_MONTH.HasValue && o.VIR_GIVE_MONTH.Value == this.GIVE_MONTH__EQUAL.Value);
                }
                if (this.RECEIVE_DATE__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.RECEIVE_DATE.HasValue && o.RECEIVE_DATE.Value == this.RECEIVE_DATE__EQUAL.Value);
                }
                if (this.RECEIVE_MONTH__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.VIR_RECEIVE_MONTH.HasValue && o.VIR_RECEIVE_MONTH.Value == this.RECEIVE_MONTH__EQUAL.Value);
                }
                if (this.APPOINTMENT_DATE__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.APPOINTMENT_DATE.HasValue && o.APPOINTMENT_DATE.Value == this.APPOINTMENT_DATE__EQUAL.Value);
                }
                if (this.APPOINTMENT_MONTH__EQUAL.HasValue)
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.VIR_APPOINTMENT_MONTH.HasValue && o.VIR_APPOINTMENT_MONTH.Value == this.APPOINTMENT_MONTH__EQUAL.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.BORROW_LOGINNAME__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.BORROW_LOGINNAME == this.BORROW_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.GIVER_LOGINNAME__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.GIVER_LOGINNAME == this.GIVER_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.RECEIVER_LOGINNAME__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.RECEIVER_LOGINNAME == this.RECEIVER_LOGINNAME__EXACT);
                }
                if (this.IS_RECEIVE.HasValue)
                {
                    if (this.IS_RECEIVE.Value)
                    {
                        listVHisMediRecordBorrowExpression.Add(o => o.RECEIVE_TIME.HasValue);
                    }
                    else
                    {
                        listVHisMediRecordBorrowExpression.Add(o => !o.RECEIVE_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.PATIENT_CODE__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.STORE_CODE__EXACT))
                {
                    listVHisMediRecordBorrowExpression.Add(o => o.STORE_CODE == this.STORE_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMediRecordBorrowExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.VIR_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }


                search.listVHisMediRecordBorrowExpression.AddRange(listVHisMediRecordBorrowExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMediRecordBorrowExpression.Clear();
                search.listVHisMediRecordBorrowExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
