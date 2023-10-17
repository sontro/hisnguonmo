using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderBlock
{
    public class HisNumOrderBlockFilterQuery : HisNumOrderBlockFilter
    {
        public HisNumOrderBlockFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_BLOCK, bool>>> listHisNumOrderBlockExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NUM_ORDER_BLOCK, bool>>>();

        

        internal HisNumOrderBlockSO Query()
        {
            HisNumOrderBlockSO search = new HisNumOrderBlockSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisNumOrderBlockExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisNumOrderBlockExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisNumOrderBlockExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisNumOrderBlockExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ROOM_TIME_ID.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.ROOM_TIME_ID == this.ROOM_TIME_ID.Value);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.ROOM_TIME_IDs != null)
                {
                    listHisNumOrderBlockExpression.Add(o => this.ROOM_TIME_IDs.Contains(o.ID));
                }
                if (this.NUM_ORDER.HasValue)
                {
                    listHisNumOrderBlockExpression.Add(o => o.NUM_ORDER == this.NUM_ORDER.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    long? number = null;
                    try
                    {
                        number = long.Parse(this.KEY_WORD);
                    }
                    catch(Exception ex)
                    {
                    }

                    if (number.HasValue)
                    {
                        listHisNumOrderBlockExpression.Add(o =>
                            o.FROM_TIME.ToLower().Contains(this.KEY_WORD) ||
                            o.TO_TIME.ToLower().Contains(this.KEY_WORD) ||
                            o.NUM_ORDER == number
                        );
                    }

                    if (!number.HasValue)
                    {
                        listHisNumOrderBlockExpression.Add(o =>
                            o.FROM_TIME.ToLower().Contains(this.KEY_WORD) ||
                            o.TO_TIME.ToLower().Contains(this.KEY_WORD));
                    }
                }

                search.listHisNumOrderBlockExpression.AddRange(listHisNumOrderBlockExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisNumOrderBlockExpression.Clear();
                search.listHisNumOrderBlockExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
