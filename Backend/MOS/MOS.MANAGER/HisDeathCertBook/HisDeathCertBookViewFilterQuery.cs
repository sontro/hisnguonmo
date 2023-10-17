using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCertBook
{
    public class HisDeathCertBookViewFilterQuery : HisDeathCertBookViewFilter
    {
        public HisDeathCertBookViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DEATH_CERT_BOOK, bool>>> listVHisDeathCertBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEATH_CERT_BOOK, bool>>>();

        

        internal HisDeathCertBookSO Query()
        {
            HisDeathCertBookSO search = new HisDeathCertBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDeathCertBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDeathCertBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDeathCertBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDeathCertBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDeathCertBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IS_OUT_OF_BOOK.HasValue)
                {
                    if (this.IS_OUT_OF_BOOK.Value)
                    {
                        listVHisDeathCertBookExpression.Add(o => o.CURRENT_DEATH_CERT_NUM >= (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                    else
                    {
                        listVHisDeathCertBookExpression.Add(o => o.CURRENT_DEATH_CERT_NUM < (o.FROM_NUM_ORDER + o.TOTAL - 1));
                    }
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDeathCertBookExpression.Add(o =>
                        o.DEATH_CERT_BOOK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEATH_CERT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisDeathCertBookExpression.AddRange(listVHisDeathCertBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDeathCertBookExpression.Clear();
                search.listVHisDeathCertBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
