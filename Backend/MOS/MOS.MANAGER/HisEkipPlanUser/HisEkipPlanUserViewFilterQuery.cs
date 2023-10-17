using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    public class HisEkipPlanUserViewFilterQuery : HisEkipPlanUserViewFilter
    {
        public HisEkipPlanUserViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EKIP_PLAN_USER, bool>>> listVHisEkipPlanUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EKIP_PLAN_USER, bool>>>();

        

        internal HisEkipPlanUserSO Query()
        {
            HisEkipPlanUserSO search = new HisEkipPlanUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisEkipPlanUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEkipPlanUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEkipPlanUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEkipPlanUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisEkipPlanUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EKIP_PLAN_ID.HasValue)
                {
                    listVHisEkipPlanUserExpression.Add(o => o.EKIP_PLAN_ID == this.EKIP_PLAN_ID.Value);
                }
                if (this.EKIP_PLAN_IDs != null)
                {
                    listVHisEkipPlanUserExpression.Add(o => this.EKIP_PLAN_IDs.Contains(o.EKIP_PLAN_ID));
                }

                search.listVHisEkipPlanUserExpression.AddRange(listVHisEkipPlanUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEkipPlanUserExpression.Clear();
                search.listVHisEkipPlanUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
