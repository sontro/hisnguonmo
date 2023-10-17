using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    public class HisSubclinicalRsAddViewFilterQuery : HisSubclinicalRsAddViewFilter
    {
        public HisSubclinicalRsAddViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SUBCLINICAL_RS_ADD, bool>>> listVHisSubclinicalRsAddExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SUBCLINICAL_RS_ADD, bool>>>();

        

        internal HisSubclinicalRsAddSO Query()
        {
            HisSubclinicalRsAddSO search = new HisSubclinicalRsAddSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.REQUEST_ROOM_ID.HasValue && this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID.Value));
                }

                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.EXECUTE_ROOM_ID.HasValue && this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID.Value));
                }

                if (this.RESULT_ROOM_ID.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.RESULT_ROOM_ID == this.RESULT_ROOM_ID.Value);
                }
                if (this.RESULT_ROOM_IDs != null)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.RESULT_ROOM_ID.HasValue && this.RESULT_ROOM_IDs.Contains(o.RESULT_ROOM_ID.Value));
                }

                if (this.RESULT_DESK_ID.HasValue)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.RESULT_DESK_ID == this.RESULT_DESK_ID.Value);
                }
                if (this.RESULT_DESK_IDs != null)
                {
                    listVHisSubclinicalRsAddExpression.Add(o => o.RESULT_DESK_ID.HasValue && this.RESULT_DESK_IDs.Contains(o.RESULT_DESK_ID.Value));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisSubclinicalRsAddExpression.Add(o => 
                        o.EXECUTE_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.REQUEST_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REQUEST_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.RESULT_DESK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.RESULT_DESK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.RESULT_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.RESULT_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisSubclinicalRsAddExpression.AddRange(listVHisSubclinicalRsAddExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSubclinicalRsAddExpression.Clear();
                search.listVHisSubclinicalRsAddExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
