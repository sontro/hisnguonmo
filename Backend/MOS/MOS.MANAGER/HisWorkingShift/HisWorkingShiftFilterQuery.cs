using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    public class HisWorkingShiftFilterQuery : HisWorkingShiftFilter
    {
        public HisWorkingShiftFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_WORKING_SHIFT, bool>>> listHisWorkingShiftExpression = new List<System.Linq.Expressions.Expression<Func<HIS_WORKING_SHIFT, bool>>>();

        

        internal HisWorkingShiftSO Query()
        {
            HisWorkingShiftSO search = new HisWorkingShiftSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisWorkingShiftExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisWorkingShiftExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisWorkingShiftExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisWorkingShiftExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisWorkingShiftExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.WORKING_SHIFT_CODE__EXACT))
                {
                    listHisWorkingShiftExpression.Add(o => o.WORKING_SHIFT_CODE == this.WORKING_SHIFT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisWorkingShiftExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.WORKING_SHIFT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.WORKING_SHIFT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisWorkingShiftExpression.AddRange(listHisWorkingShiftExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisWorkingShiftExpression.Clear();
                search.listHisWorkingShiftExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
