using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionExp
{
    public class HisTransactionExpFilterQuery : HisTransactionExpFilter
    {
        public HisTransactionExpFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRANSACTION_EXP, bool>>> listHisTransactionExpExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSACTION_EXP, bool>>>();

        

        internal HisTransactionExpSO Query()
        {
            HisTransactionExpSO search = new HisTransactionExpSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTransactionExpExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTransactionExpExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTransactionExpExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTransactionExpExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.EXP_MEST_CODE__EXACT))
                {
                    listHisTransactionExpExpression.Add(o => o.TDL_EXP_MEST_CODE == this.EXP_MEST_CODE__EXACT);
                }
                if (this.TRANSACTION_ID.HasValue)
                {
                    listHisTransactionExpExpression.Add(o => o.TRANSACTION_ID == this.TRANSACTION_ID.Value);
                }

                search.listHisTransactionExpExpression.AddRange(listHisTransactionExpExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTransactionExpExpression.Clear();
                search.listHisTransactionExpExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
