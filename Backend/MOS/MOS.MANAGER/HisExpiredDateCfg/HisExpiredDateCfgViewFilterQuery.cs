using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    public class HisExpiredDateCfgViewFilterQuery : HisExpiredDateCfgViewFilter
    {
        public HisExpiredDateCfgViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXPIRED_DATE_CFG, bool>>> listVHisExpiredDateCfgExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXPIRED_DATE_CFG, bool>>>();

        

        internal HisExpiredDateCfgSO Query()
        {
            HisExpiredDateCfgSO search = new HisExpiredDateCfgSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExpiredDateCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpiredDateCfgExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisExpiredDateCfgExpression.AddRange(listVHisExpiredDateCfgExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpiredDateCfgExpression.Clear();
                search.listVHisExpiredDateCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
