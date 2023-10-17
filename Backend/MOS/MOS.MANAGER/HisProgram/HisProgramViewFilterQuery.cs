using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProgram
{
    public class HisProgramViewFilterQuery : HisProgramViewFilter
    {
        public HisProgramViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PROGRAM, bool>>> listVHisProgramExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PROGRAM, bool>>>();

        

        internal HisProgramSO Query()
        {
            HisProgramSO search = new HisProgramSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisProgramExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisProgramExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisProgramExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisProgramExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisProgramExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.BRANCH_ID.HasValue && o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisProgramExpression.Add(o => o.BRANCH_ID.HasValue && this.BRANCH_IDs.Contains(o.BRANCH_ID.Value));
                }
                if (this.DATA_STORE_ID.HasValue)
                {
                    listVHisProgramExpression.Add(o => o.DATA_STORE_ID.HasValue && o.DATA_STORE_ID == this.DATA_STORE_ID.Value);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    listVHisProgramExpression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }

                search.listVHisProgramExpression.AddRange(listVHisProgramExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisProgramExpression.Clear();
                search.listVHisProgramExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
