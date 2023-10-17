using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    public class HisPositionFilterQuery : HisPositionFilter
    {
        public HisPositionFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_POSITION, bool>>> listHisPositionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_POSITION, bool>>>();

        

        internal HisPositionSO Query()
        {
            HisPositionSO search = new HisPositionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPositionExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPositionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPositionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPositionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPositionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPositionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPositionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPositionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPositionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPositionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisPositionExpression.Add(o =>
                        o.POSITION_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.POSITION_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                #endregion
                
                search.listHisPositionExpression.AddRange(listHisPositionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPositionExpression.Clear();
                search.listHisPositionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
