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
    public class HisDocumentBookViewFilterQuery : HisDocumentBookViewFilter
    {
        public HisDocumentBookViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DOCUMENT_BOOK, bool>>> listVHisDocumentBookExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DOCUMENT_BOOK, bool>>>();



        internal HisDocumentBookSO Query()
        {
            HisDocumentBookSO search = new HisDocumentBookSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisDocumentBookExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDocumentBookExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDocumentBookExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDocumentBookExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDocumentBookExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.DOCUMENT_BOOK_CODE__EXACT))
                {
                    listVHisDocumentBookExpression.Add(o => o.DOCUMENT_BOOK_CODE == this.DOCUMENT_BOOK_CODE__EXACT);
                }
                if (this.FOR_SICK_BHXH.HasValue)
                {
                    if (this.FOR_SICK_BHXH.Value)
                    {
                        listVHisDocumentBookExpression.Add(o => o.IS_SICK_BHXH.HasValue && o.IS_SICK_BHXH.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisDocumentBookExpression.Add(o => !o.IS_SICK_BHXH.HasValue || o.IS_SICK_BHXH.Value != Constant.IS_TRUE);
                    }
                }

                if (this.IS_OUT_NUM_ORDER.HasValue)
                {
                    if (this.IS_OUT_NUM_ORDER.Value)
                    {
                        listVHisDocumentBookExpression.Add(o => o.CURRENT_NUM_ORDER.HasValue && o.CURRENT_NUM_ORDER.Value >= (o.FROM_NUM_ORDER + o.TOTAL_NUM_ORDER - 1));
                    }
                    else
                    {
                        listVHisDocumentBookExpression.Add(o => !o.CURRENT_NUM_ORDER.HasValue || o.CURRENT_NUM_ORDER.Value < (o.FROM_NUM_ORDER + o.TOTAL_NUM_ORDER - 1));
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDocumentBookExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.DOCUMENT_BOOK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DOCUMENT_BOOK_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisDocumentBookExpression.AddRange(listVHisDocumentBookExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDocumentBookExpression.Clear();
                search.listVHisDocumentBookExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
