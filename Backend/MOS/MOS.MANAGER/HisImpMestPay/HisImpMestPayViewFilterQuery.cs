using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPay
{
    public class HisImpMestPayViewFilterQuery : HisImpMestPayViewFilter
    {
        public HisImpMestPayViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PAY, bool>>> listVHisImpMestPayExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PAY, bool>>>();



        internal HisImpMestPaySO Query()
        {
            HisImpMestPaySO search = new HisImpMestPaySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisImpMestPayExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisImpMestPayExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisImpMestPayExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisImpMestPayExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisImpMestPayExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.IMP_MEST_PROPOSE_ID.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.IMP_MEST_PROPOSE_ID == this.IMP_MEST_PROPOSE_ID.Value);
                }
                if (this.IMP_MEST_PROPOSE_IDs != null)
                {
                    listVHisImpMestPayExpression.Add(o => this.IMP_MEST_PROPOSE_IDs.Contains(o.IMP_MEST_PROPOSE_ID));
                }
                if (this.PAY_FORM_ID.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.PAY_FORM_ID.HasValue && o.PAY_FORM_ID == this.PAY_FORM_ID.Value);
                }
                if (this.PAY_FORM_IDs != null)
                {
                    listVHisImpMestPayExpression.Add(o => o.PAY_FORM_ID.HasValue && this.PAY_FORM_IDs.Contains(o.PAY_FORM_ID.Value));
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.SUPPLIER_ID == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisImpMestPayExpression.Add(o => this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID));
                }

                if (this.PAY_TIME_FROM.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.PAY_TIME >= this.PAY_TIME_FROM.Value);
                }
                if (this.PAY_TIME_TO.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.PAY_TIME <= this.PAY_TIME_TO.Value);
                }
                if (this.NEXT_PAY_TIME_FROM.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.NEXT_PAY_TIME.HasValue && o.NEXT_PAY_TIME.Value >= this.NEXT_PAY_TIME_FROM.Value);
                }
                if (this.NEXT_PAY_TIME_TO.HasValue)
                {
                    listVHisImpMestPayExpression.Add(o => o.NEXT_PAY_TIME.HasValue && o.NEXT_PAY_TIME.Value <= this.NEXT_PAY_TIME_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.PAYER_LOGINNAME__EXACT))
                {
                    listVHisImpMestPayExpression.Add(o => o.PAYER_LOGINNAME == this.PAYER_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SUPPLIER_CODE__EXACT))
                {
                    listVHisImpMestPayExpression.Add(o => o.SUPPLIER_CODE == this.SUPPLIER_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisImpMestPayExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PAYER_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.PAYER_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.SUPPLIER_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SUPPLIER_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisImpMestPayExpression.AddRange(listVHisImpMestPayExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisImpMestPayExpression.Clear();
                search.listVHisImpMestPayExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
