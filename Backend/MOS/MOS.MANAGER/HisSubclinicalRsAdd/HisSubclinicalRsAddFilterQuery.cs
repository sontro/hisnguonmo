using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    public class HisSubclinicalRsAddFilterQuery : HisSubclinicalRsAddFilter
    {
        public HisSubclinicalRsAddFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SUBCLINICAL_RS_ADD, bool>>> listHisSubclinicalRsAddExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SUBCLINICAL_RS_ADD, bool>>>();

        

        internal HisSubclinicalRsAddSO Query()
        {
            HisSubclinicalRsAddSO search = new HisSubclinicalRsAddSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSubclinicalRsAddExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID.Value));
                }

                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID.Value));
                }

                if (this.RESULT_ROOM_ID.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.RESULT_ROOM_ID == this.RESULT_ROOM_ID.Value);
                }
                if (this.RESULT_ROOM_IDs != null)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.RESULT_ROOM_ID.HasValue && this.RESULT_ROOM_IDs.Contains(o.RESULT_ROOM_ID.Value));
                }

                if (this.RESULT_DESK_ID.HasValue)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.RESULT_DESK_ID == this.RESULT_DESK_ID.Value);
                }
                if (this.RESULT_DESK_IDs != null)
                {
                    listHisSubclinicalRsAddExpression.Add(o => o.RESULT_DESK_ID.HasValue && this.RESULT_DESK_IDs.Contains(o.RESULT_DESK_ID.Value));
                }
                search.listHisSubclinicalRsAddExpression.AddRange(listHisSubclinicalRsAddExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSubclinicalRsAddExpression.Clear();
                search.listHisSubclinicalRsAddExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
