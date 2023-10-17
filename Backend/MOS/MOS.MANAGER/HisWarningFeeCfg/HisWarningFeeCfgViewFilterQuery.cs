using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    public class HisWarningFeeCfgViewFilterQuery : HisWarningFeeCfgViewFilter
    {
        public HisWarningFeeCfgViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_WARNING_FEE_CFG, bool>>> listVHisWarningFeeCfgExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_WARNING_FEE_CFG, bool>>>();

        

        internal HisWarningFeeCfgSO Query()
        {
            HisWarningFeeCfgSO search = new HisWarningFeeCfgSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisWarningFeeCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisWarningFeeCfgExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisWarningFeeCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisWarningFeeCfgExpression.AddRange(listVHisWarningFeeCfgExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisWarningFeeCfgExpression.Clear();
                search.listVHisWarningFeeCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
