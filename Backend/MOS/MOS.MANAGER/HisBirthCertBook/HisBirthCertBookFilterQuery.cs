using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBirthCertBook
{
    public class HisBirthCertBookFilterQuery : HisBirthCertBookFilter
    {
        public HisBirthCertBookFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BIRTH_CERT_BOOK, bool>>> listHisBirthCertBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BIRTH_CERT_BOOK, bool>>>();

        

        internal HisBirthCertBookSO Query()
        {
            HisBirthCertBookSO search = new HisBirthCertBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisBirthCertBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBirthCertBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBirthCertBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBirthCertBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBirthCertBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisBirthCertBookExpression.Add(o =>
                        o.BIRTH_CERT_BOOK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BIRTH_CERT_BOOK_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisBirthCertBookExpression.AddRange(listHisBirthCertBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBirthCertBookExpression.Clear();
                search.listHisBirthCertBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
