using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    public class HisEmployeeFilterQuery : HisEmployeeFilter
    {
        public HisEmployeeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE, bool>>> listHisEmployeeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMPLOYEE, bool>>>();



        internal HisEmployeeSO Query()
        {
            HisEmployeeSO search = new HisEmployeeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisEmployeeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEmployeeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEmployeeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEmployeeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisEmployeeExpression.Add(o =>
                        o.DIPLOMA.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_EMAIL.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_MOBILE.ToLower().Contains(this.KEY_WORD) ||
                        o.ACCOUNT_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.BANK.ToLower().Contains(this.KEY_WORD) ||
                        o.TITLE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!String.IsNullOrEmpty(this.LOGINNAME__EXACT))
                {
                    listHisEmployeeExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.MEDICINE_TYPE_RANK.HasValue)
                {
                    listHisEmployeeExpression.Add(o => o.MEDICINE_TYPE_RANK.HasValue && o.MEDICINE_TYPE_RANK.Value == this.MEDICINE_TYPE_RANK.Value);
                }
                if (this.LOGINNAMEs != null)
                {
                    listHisEmployeeExpression.Add(o => this.LOGINNAMEs.Contains(o.LOGINNAME));
                }

                search.listHisEmployeeExpression.AddRange(listHisEmployeeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEmployeeExpression.Clear();
                search.listHisEmployeeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
