using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    public class HisDebateEkipUserFilterQuery : HisDebateEkipUserFilter
    {
        public HisDebateEkipUserFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_EKIP_USER, bool>>> listHisDebateEkipUserExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DEBATE_EKIP_USER, bool>>>();

        

        internal HisDebateEkipUserSO Query()
        {
            HisDebateEkipUserSO search = new HisDebateEkipUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDebateEkipUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDebateEkipUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDebateEkipUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDebateEkipUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DEBATE_ID.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.DEBATE_ID == this.DEBATE_ID.Value);
                }
                if (this.EXECUTE_ROLE_ID.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.EXECUTE_ROLE_ID == this.EXECUTE_ROLE_ID.Value);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisDebateEkipUserExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDebateEkipUserExpression.Add(o =>
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisDebateEkipUserExpression.AddRange(listHisDebateEkipUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDebateEkipUserExpression.Clear();
                search.listHisDebateEkipUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
