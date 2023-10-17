using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    public class HisMediRecordBorrowFilterQuery : HisMediRecordBorrowFilter
    {
        public HisMediRecordBorrowFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_BORROW, bool>>> listHisMediRecordBorrowExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD_BORROW, bool>>>();

        

        internal HisMediRecordBorrowSO Query()
        {
            HisMediRecordBorrowSO search = new HisMediRecordBorrowSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMediRecordBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMediRecordBorrowExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDI_RECORD_ID.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.MEDI_RECORD_ID == this.MEDI_RECORD_ID.Value);
                }
                if (this.MEDI_RECORD_IDs != null)
                {
                    listHisMediRecordBorrowExpression.Add(o => this.MEDI_RECORD_IDs.Contains(o.MEDI_RECORD_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listHisMediRecordBorrowExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.GIVE_DATE_FROM.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.GIVE_DATE >= this.GIVE_DATE_FROM.Value);
                }
                if (this.GIVE_DATE_TO.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.GIVE_DATE <= this.GIVE_DATE_TO.Value);
                }
                if (this.RECEIVE_DATE_FROM.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.RECEIVE_DATE.HasValue && o.RECEIVE_DATE.Value >= this.RECEIVE_DATE_FROM.Value);
                }
                if (this.RECEIVE_DATE_TO.HasValue)
                {
                    listHisMediRecordBorrowExpression.Add(o => o.RECEIVE_DATE.HasValue && o.RECEIVE_DATE.Value <= this.RECEIVE_DATE_TO.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.BORROW_LOGINNAME__EXACT))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.BORROW_LOGINNAME == this.BORROW_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.GIVER_LOGINNAME__EXACT))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.GIVER_LOGINNAME == this.GIVER_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.RECEIVER_LOGINNAME__EXACT))
                {
                    listHisMediRecordBorrowExpression.Add(o => o.RECEIVER_LOGINNAME == this.RECEIVER_LOGINNAME__EXACT);
                }
                if (this.IS_RECEIVE.HasValue)
                {
                    if (this.IS_RECEIVE.Value)
                    {
                        listHisMediRecordBorrowExpression.Add(o => o.RECEIVE_TIME.HasValue);
                    }
                    else
                    {
                        listHisMediRecordBorrowExpression.Add(o => !o.RECEIVE_TIME.HasValue);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisMediRecordBorrowExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORROW_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GIVER_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RECEIVER_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }


                search.listHisMediRecordBorrowExpression.AddRange(listHisMediRecordBorrowExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMediRecordBorrowExpression.Clear();
                search.listHisMediRecordBorrowExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
