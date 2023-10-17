using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    public class HisHoreHohaFilterQuery : HisHoreHohaFilter
    {
        public HisHoreHohaFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HORE_HOHA, bool>>> listHisHoreHohaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_HOHA, bool>>>();

        

        internal HisHoreHohaSO Query()
        {
            HisHoreHohaSO search = new HisHoreHohaSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisHoreHohaExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHoreHohaExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHoreHohaExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHoreHohaExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisHoreHohaExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.HOLD_RETURN_ID.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.HOLD_RETURN_ID == this.HOLD_RETURN_ID.Value);
                }
                if (this.HOLD_RETURN_IDs != null)
                {
                    listHisHoreHohaExpression.Add(o => this.HOLD_RETURN_IDs.Contains(o.HOLD_RETURN_ID));
                }
                if (this.HORE_HANDOVER_ID.HasValue)
                {
                    listHisHoreHohaExpression.Add(o => o.HORE_HANDOVER_ID == this.HORE_HANDOVER_ID.Value);
                }
                if (this.HORE_HANDOVER_IDs != null)
                {
                    listHisHoreHohaExpression.Add(o => this.HORE_HANDOVER_IDs.Contains(o.HORE_HANDOVER_ID));
                }

                search.listHisHoreHohaExpression.AddRange(listHisHoreHohaExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHoreHohaExpression.Clear();
                search.listHisHoreHohaExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
