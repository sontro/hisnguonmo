using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttTable
{
    public class HisPtttTableViewFilterQuery : HisPtttTableViewFilter
    {
        public HisPtttTableViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_TABLE, bool>>> listVHisPtttTableExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_TABLE, bool>>>();

        

        internal HisPtttTableSO Query()
        {
            HisPtttTableSO search = new HisPtttTableSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPtttTableExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPtttTableExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPtttTableExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPtttTableExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPtttTableExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisPtttTableExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisPtttTableExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisPtttTableExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PTTT_TABLE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PTTT_TABLE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisPtttTableExpression.AddRange(listVHisPtttTableExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPtttTableExpression.Clear();
                search.listVHisPtttTableExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
