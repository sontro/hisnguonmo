using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployee
{
    public class HisEmployeeViewFilterQuery : HisEmployeeViewFilter
    {
        public HisEmployeeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE, bool>>> listVHisEmployeeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMPLOYEE, bool>>>();



        internal HisEmployeeSO Query()
        {
            HisEmployeeSO search = new HisEmployeeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisEmployeeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEmployeeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEmployeeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEmployeeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisEmployeeExpression.Add(o =>
                        o.DIPLOMA.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!String.IsNullOrEmpty(this.LOGINNAME__EXACT))
                {
                    listVHisEmployeeExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.MEDICINE_TYPE_RANK.HasValue)
                {
                    listVHisEmployeeExpression.Add(o => o.MEDICINE_TYPE_RANK.HasValue && o.MEDICINE_TYPE_RANK.Value == this.MEDICINE_TYPE_RANK.Value);
                }
                if (this.LOGINNAMEs != null)
                {
                    listVHisEmployeeExpression.Add(o => this.LOGINNAMEs.Contains(o.LOGINNAME));
                }

                search.listVHisEmployeeExpression.AddRange(listVHisEmployeeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEmployeeExpression.Clear();
                search.listVHisEmployeeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
