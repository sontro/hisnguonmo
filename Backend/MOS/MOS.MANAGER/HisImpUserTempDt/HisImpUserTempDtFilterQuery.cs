using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    public class HisImpUserTempDtFilterQuery : HisImpUserTempDtFilter
    {
        public HisImpUserTempDtFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP_DT, bool>>> listHisImpUserTempDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP_DT, bool>>>();

        

        internal HisImpUserTempDtSO Query()
        {
            HisImpUserTempDtSO search = new HisImpUserTempDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisImpUserTempDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisImpUserTempDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisImpUserTempDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisImpUserTempDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisImpUserTempDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.IMP_USER_TEMP_ID.HasValue)
                {
                    listHisImpUserTempDtExpression.Add(o => o.IMP_USER_TEMP_ID == this.IMP_USER_TEMP_ID.Value);
                }

                search.listHisImpUserTempDtExpression.AddRange(listHisImpUserTempDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisImpUserTempDtExpression.Clear();
                search.listHisImpUserTempDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
