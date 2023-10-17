using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebate
{
    public class HisDebateViewFilterQuery : HisDebateViewFilter
    {
        public HisDebateViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE, bool>>> listVHisDebateExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DEBATE, bool>>>();

        

        internal HisDebateSO Query()
        {
            HisDebateSO search = new HisDebateSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDebateExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDebateExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDebateExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDebateExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisDebateExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }

                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }

                if (this.TREATMENT_IDs != null)
                {
                    listVHisDebateExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisDebateExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID.Value));
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisDebateExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (this.DEBATE_TIME_FROM.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.DEBATE_TIME.Value >= this.DEBATE_TIME_FROM.Value);
                }
                if (this.DEBATE_TIME_TO.HasValue)
                {
                    listVHisDebateExpression.Add(o => o.DEBATE_TIME.Value <= this.DEBATE_TIME_TO.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.INVITE_USER_LOGINNAME))
                {
                    this.INVITE_USER_LOGINNAME = this.INVITE_USER_LOGINNAME.ToLower().Trim();
                    listVHisDebateExpression.Add(o => o.INVITE_USER_LOGINNAME.ToLower().Contains(this.INVITE_USER_LOGINNAME));
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDebateExpression.Add(o => 
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisDebateExpression.AddRange(listVHisDebateExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDebateExpression.Clear();
                search.listVHisDebateExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
