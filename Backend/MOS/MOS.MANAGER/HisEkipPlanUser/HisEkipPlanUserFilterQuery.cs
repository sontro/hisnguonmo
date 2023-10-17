using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    public class HisEkipPlanUserFilterQuery : HisEkipPlanUserFilter
    {
        public HisEkipPlanUserFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN_USER, bool>>> listHisEkipPlanUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EKIP_PLAN_USER, bool>>>();

        

        internal HisEkipPlanUserSO Query()
        {
            HisEkipPlanUserSO search = new HisEkipPlanUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEkipPlanUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEkipPlanUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEkipPlanUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEkipPlanUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisEkipPlanUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EKIP_PLAN_ID.HasValue)
                {
                    listHisEkipPlanUserExpression.Add(o => o.EKIP_PLAN_ID == this.EKIP_PLAN_ID.Value);
                }
                if (this.EKIP_PLAN_IDs != null)
                {
                    listHisEkipPlanUserExpression.Add(o => this.EKIP_PLAN_IDs.Contains(o.EKIP_PLAN_ID));
                }

                search.listHisEkipPlanUserExpression.AddRange(listHisEkipPlanUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEkipPlanUserExpression.Clear();
                search.listHisEkipPlanUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
