using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    public class HisDebateInviteUserViewFilterQuery : HisDebateInviteUserViewFilter
    {
        public HisDebateInviteUserViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_INVITE_USER, bool>>> listVHisDebateInviteUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_INVITE_USER, bool>>>();

        

        internal HisDebateInviteUserSO Query()
        {
            HisDebateInviteUserSO search = new HisDebateInviteUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDebateInviteUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDebateInviteUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDebateInviteUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDebateInviteUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDebateInviteUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisDebateInviteUserExpression.AddRange(listVHisDebateInviteUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDebateInviteUserExpression.Clear();
                search.listVHisDebateInviteUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
