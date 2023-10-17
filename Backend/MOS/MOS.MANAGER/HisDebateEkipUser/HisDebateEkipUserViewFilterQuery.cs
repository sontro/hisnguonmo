using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    public class HisDebateEkipUserViewFilterQuery : HisDebateEkipUserViewFilter
    {
        public HisDebateEkipUserViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_EKIP_USER, bool>>> listVHisDebateEkipUserExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE_EKIP_USER, bool>>>();

        

        internal HisDebateEkipUserSO Query()
        {
            HisDebateEkipUserSO search = new HisDebateEkipUserSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDebateEkipUserExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDebateEkipUserExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDebateEkipUserExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDebateEkipUserExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DEBATE_ID.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.DEBATE_ID == this.DEBATE_ID.Value);
                }
                if (this.EXECUTE_ROLE_ID.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.EXECUTE_ROLE_ID == this.EXECUTE_ROLE_ID.Value);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisDebateEkipUserExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDebateEkipUserExpression.Add(o =>
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROLE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.USERNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisDebateEkipUserExpression.AddRange(listVHisDebateEkipUserExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDebateEkipUserExpression.Clear();
                search.listVHisDebateEkipUserExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
