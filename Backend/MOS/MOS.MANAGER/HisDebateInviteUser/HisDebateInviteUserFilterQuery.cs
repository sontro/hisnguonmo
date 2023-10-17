using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    public class HisDebateInviteUserFilterQuery : HisDebateInviteUserFilter
    {
        public HisDebateInviteUserFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_INVITE_USER, bool>>> listHisDebateInviteUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_INVITE_USER, bool>>>();

        

        internal HisDebateInviteUserSO Query()
        {
            HisDebateInviteUserSO search = new HisDebateInviteUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDebateInviteUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDebateInviteUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDebateInviteUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDebateInviteUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.DEBATE_ID.HasValue)
                {
                    listHisDebateInviteUserExpression.Add(o => o.DEBATE_ID == this.DEBATE_ID.Value);
                }

                search.listHisDebateInviteUserExpression.AddRange(listHisDebateInviteUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDebateInviteUserExpression.Clear();
                search.listHisDebateInviteUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
