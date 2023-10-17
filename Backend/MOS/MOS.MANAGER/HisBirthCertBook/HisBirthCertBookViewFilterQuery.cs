using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    public class HisBirthCertBookViewFilterQuery : HisBirthCertBookViewFilter
    {
        public HisBirthCertBookViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BIRTH_CERT_BOOK, bool>>> listVHisBirthCertBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BIRTH_CERT_BOOK, bool>>>();

        

        internal HisBirthCertBookSO Query()
        {
            HisBirthCertBookSO search = new HisBirthCertBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBirthCertBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBirthCertBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBirthCertBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBirthCertBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBirthCertBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.IS_OUT_OF_BOOK.HasValue && this.IS_OUT_OF_BOOK.Value)
                {
                    listVHisBirthCertBookExpression.Add(o => o.CURRENT_BIRTH_CERT_NUM >= (o.FROM_NUM_ORDER + o.TOTAL - 1));
                }
                if (this.IS_OUT_OF_BOOK.HasValue && !this.IS_OUT_OF_BOOK.Value)
                {
                    listVHisBirthCertBookExpression.Add(o => o.CURRENT_BIRTH_CERT_NUM < (o.FROM_NUM_ORDER + o.TOTAL - 1));
                }

                search.listVHisBirthCertBookExpression.AddRange(listVHisBirthCertBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBirthCertBookExpression.Clear();
                search.listVHisBirthCertBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
