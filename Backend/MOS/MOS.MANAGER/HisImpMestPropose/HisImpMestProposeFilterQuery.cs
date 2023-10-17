using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPropose
{
    public class HisImpMestProposeFilterQuery : HisImpMestProposeFilter
    {
        public HisImpMestProposeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PROPOSE, bool>>> listHisImpMestProposeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_MEST_PROPOSE, bool>>>();

        

        internal HisImpMestProposeSO Query()
        {
            HisImpMestProposeSO search = new HisImpMestProposeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisImpMestProposeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisImpMestProposeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisImpMestProposeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisImpMestProposeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisImpMestProposeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.PROPOSE_ROOM_ID.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.PROPOSE_ROOM_ID == this.PROPOSE_ROOM_ID.Value);
                }
                if (this.PROPOSE_ROOM_IDs != null)
                {
                    listHisImpMestProposeExpression.Add(o => this.PROPOSE_ROOM_IDs.Contains(o.PROPOSE_ROOM_ID));
                }

                if (this.PROPOSE_DEPARTMENT_ID.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.PROPOSE_DEPARTMENT_ID == this.PROPOSE_DEPARTMENT_ID.Value);
                }
                if (this.PROPOSE_DEPARTMENT_IDs != null)
                {
                    listHisImpMestProposeExpression.Add(o => this.PROPOSE_DEPARTMENT_IDs.Contains(o.PROPOSE_DEPARTMENT_ID));
                }

                if (this.SUPPLIER_ID.HasValue)
                {
                    listHisImpMestProposeExpression.Add(o => o.SUPPLIER_ID == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listHisImpMestProposeExpression.Add(o => this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID));
                }
                if (!String.IsNullOrEmpty(this.IMP_MEST_PROPOSE_CODE__EXACT))
                {
                    listHisImpMestProposeExpression.Add(o => o.IMP_MEST_PROPOSE_CODE == this.IMP_MEST_PROPOSE_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisImpMestProposeExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.IMP_MEST_PROPOSE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PROPOSE_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.PROPOSE_USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisImpMestProposeExpression.AddRange(listHisImpMestProposeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisImpMestProposeExpression.Clear();
                search.listHisImpMestProposeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
