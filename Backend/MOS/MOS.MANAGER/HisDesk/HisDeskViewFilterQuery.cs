using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    public class HisDeskViewFilterQuery : HisDeskViewFilter
    {
        public HisDeskViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DESK, bool>>> listVHisDeskExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DESK, bool>>>();

        

        internal HisDeskSO Query()
        {
            HisDeskSO search = new HisDeskSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisDeskExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDeskExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDeskExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDeskExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisDeskExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDeskExpression.Add(o =>
                        o.DESK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DESK_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_NAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisDeskExpression.AddRange(listVHisDeskExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDeskExpression.Clear();
                search.listVHisDeskExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
