using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocumentBook
{
    public class HisDocumentBookFilterQuery : HisDocumentBookFilter
    {
        public HisDocumentBookFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DOCUMENT_BOOK, bool>>> listHisDocumentBookExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DOCUMENT_BOOK, bool>>>();



        internal HisDocumentBookSO Query()
        {
            HisDocumentBookSO search = new HisDocumentBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisDocumentBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDocumentBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDocumentBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDocumentBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDocumentBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.DOCUMENT_BOOK_CODE__EXACT))
                {
                    listHisDocumentBookExpression.Add(o => o.DOCUMENT_BOOK_CODE == this.DOCUMENT_BOOK_CODE__EXACT);
                }
                if (this.FOR_SICK_BHXH.HasValue)
                {
                    if (this.FOR_SICK_BHXH.Value)
                    {
                        listHisDocumentBookExpression.Add(o => o.IS_SICK_BHXH.HasValue && o.IS_SICK_BHXH.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisDocumentBookExpression.Add(o => !o.IS_SICK_BHXH.HasValue || o.IS_SICK_BHXH.Value != Constant.IS_TRUE);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDocumentBookExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.DOCUMENT_BOOK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DOCUMENT_BOOK_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisDocumentBookExpression.AddRange(listHisDocumentBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDocumentBookExpression.Clear();
                search.listHisDocumentBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
