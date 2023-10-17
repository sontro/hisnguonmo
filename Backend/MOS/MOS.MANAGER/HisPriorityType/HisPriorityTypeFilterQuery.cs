using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPriorityType
{
    public class HisPriorityTypeFilterQuery : HisPriorityTypeFilter
    {
        public HisPriorityTypeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PRIORITY_TYPE, bool>>> listHisPriorityTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PRIORITY_TYPE, bool>>>();

        

        internal HisPriorityTypeSO Query()
        {
            HisPriorityTypeSO search = new HisPriorityTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPriorityTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPriorityTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPriorityTypeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPriorityTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPriorityTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisPriorityTypeExpression.Add(o =>
                        o.PRIORITY_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PRIORITY_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.IS_FOR_PRESCRIPTION.HasValue)
                {
                    if (this.IS_FOR_PRESCRIPTION.Value)
                    {
                        listHisPriorityTypeExpression.Add(o => o.IS_FOR_PRESCRIPTION == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisPriorityTypeExpression.Add(o => o.IS_FOR_PRESCRIPTION != Constant.IS_TRUE);
                    }
                }

                search.listHisPriorityTypeExpression.AddRange(listHisPriorityTypeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPriorityTypeExpression.Clear();
                search.listHisPriorityTypeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
