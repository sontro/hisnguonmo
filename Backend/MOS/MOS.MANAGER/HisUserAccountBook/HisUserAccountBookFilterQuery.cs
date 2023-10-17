using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    public class HisUserAccountBookFilterQuery : HisUserAccountBookFilter
    {
        public HisUserAccountBookFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_USER_ACCOUNT_BOOK, bool>>> listHisUserAccountBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_USER_ACCOUNT_BOOK, bool>>>();

        

        internal HisUserAccountBookSO Query()
        {
            HisUserAccountBookSO search = new HisUserAccountBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisUserAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisUserAccountBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisUserAccountBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisUserAccountBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisUserAccountBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ACCOUNT_BOOK_ID.HasValue)
                {
                    listHisUserAccountBookExpression.Add(o => o.ACCOUNT_BOOK_ID == this.ACCOUNT_BOOK_ID.Value);
                }
                if (this.ACCOUNT_BOOK_IDs != null)
                {
                    listHisUserAccountBookExpression.Add(o => this.ACCOUNT_BOOK_IDs.Contains(o.ACCOUNT_BOOK_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.LOGINNAME__EXACT))
                {
                    listHisUserAccountBookExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }

                search.listHisUserAccountBookExpression.AddRange(listHisUserAccountBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisUserAccountBookExpression.Clear();
                search.listHisUserAccountBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
