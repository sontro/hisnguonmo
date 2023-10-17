using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    public class HisImpUserTempDtViewFilterQuery : HisImpUserTempDtViewFilter
    {
        public HisImpUserTempDtViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_USER_TEMP_DT, bool>>> listVHisImpUserTempDtExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_IMP_USER_TEMP_DT, bool>>>();

        

        internal HisImpUserTempDtSO Query()
        {
            HisImpUserTempDtSO search = new HisImpUserTempDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisImpUserTempDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisImpUserTempDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisImpUserTempDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisImpUserTempDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisImpUserTempDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.IMP_USER_TEMP_ID.HasValue)
                {
                    listVHisImpUserTempDtExpression.Add(o => o.IMP_USER_TEMP_ID == this.IMP_USER_TEMP_ID.Value);
                }

                search.listVHisImpUserTempDtExpression.AddRange(listVHisImpUserTempDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisImpUserTempDtExpression.Clear();
                search.listVHisImpUserTempDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
