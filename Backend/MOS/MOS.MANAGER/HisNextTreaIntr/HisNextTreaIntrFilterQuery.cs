using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNextTreaIntr
{
    public class HisNextTreaIntrFilterQuery : HisNextTreaIntrFilter
    {
        public HisNextTreaIntrFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_NEXT_TREA_INTR, bool>>> listHisNextTreaIntrExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NEXT_TREA_INTR, bool>>>();

        

        internal HisNextTreaIntrSO Query()
        {
            HisNextTreaIntrSO search = new HisNextTreaIntrSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisNextTreaIntrExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisNextTreaIntrExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisNextTreaIntrExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisNextTreaIntrExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisNextTreaIntrExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisNextTreaIntrExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.NEXT_TREA_INTR_CODE__EXACT))
                {
                    listHisNextTreaIntrExpression.Add(o => o.NEXT_TREA_INTR_CODE == this.NEXT_TREA_INTR_CODE__EXACT);
                }

                search.listHisNextTreaIntrExpression.AddRange(listHisNextTreaIntrExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisNextTreaIntrExpression.Clear();
                search.listHisNextTreaIntrExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
