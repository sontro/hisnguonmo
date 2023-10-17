using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    public class HisImpMestProposeViewFilterQuery : HisImpMestProposeViewFilter
    {
        public HisImpMestProposeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PROPOSE, bool>>> listVHisImpMestProposeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_MEST_PROPOSE, bool>>>();

        internal HisImpMestProposeSO Query()
        {
            HisImpMestProposeSO search = new HisImpMestProposeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisImpMestProposeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisImpMestProposeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisImpMestProposeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisImpMestProposeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisImpMestProposeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.PROPOSE_ROOM_ID.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.PROPOSE_ROOM_ID == this.PROPOSE_ROOM_ID.Value);
                }
                if (this.PROPOSE_ROOM_IDs != null)
                {
                    listVHisImpMestProposeExpression.Add(o => this.PROPOSE_ROOM_IDs.Contains(o.PROPOSE_ROOM_ID));
                }

                if (this.PROPOSE_DEPARTMENT_ID.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.PROPOSE_DEPARTMENT_ID == this.PROPOSE_DEPARTMENT_ID.Value);
                }
                if (this.PROPOSE_DEPARTMENT_IDs != null)
                {
                    listVHisImpMestProposeExpression.Add(o => this.PROPOSE_DEPARTMENT_IDs.Contains(o.PROPOSE_DEPARTMENT_ID));
                }

                if (this.SUPPLIER_ID.HasValue)
                {
                    listVHisImpMestProposeExpression.Add(o => o.SUPPLIER_ID == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisImpMestProposeExpression.Add(o => this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID));
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_PROPOSE_CODE__EXACT))
                {
                    listVHisImpMestProposeExpression.Add(o => o.IMP_MEST_PROPOSE_CODE == this.IMP_MEST_PROPOSE_CODE__EXACT);
                }

                if (this.PAY_STATUS.HasValue)
                {
                    if (this.PAY_STATUS.Value == HisImpMestProposeConstant.PAY_STATUS__NOT_YET)
                    {
                        listVHisImpMestProposeExpression.Add(o => !o.PAYED.HasValue || o.PAYED.Value == 0);
                    }
                    else if (this.PAY_STATUS.Value == HisImpMestProposeConstant.PAY_STATUS__PAYING)
                    {
                        listVHisImpMestProposeExpression.Add(o => o.PAYED.HasValue && o.PAYED.Value > 0 && (o.TOTAL_PAY ?? 0) > o.PAYED.Value);
                    }
                    else if (this.PAY_STATUS.Value == HisImpMestProposeConstant.PAY_STATUS__NOT_YET)
                    {
                        listVHisImpMestProposeExpression.Add(o => o.PAYED.HasValue && o.PAYED.Value >= (o.TOTAL_PAY ?? 0));
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisImpMestProposeExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_MEST_PROPOSE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PROPOSE_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.PROPOSE_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.SUPPLIER_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SUPPLIER_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisImpMestProposeExpression.AddRange(listVHisImpMestProposeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisImpMestProposeExpression.Clear();
                search.listVHisImpMestProposeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
