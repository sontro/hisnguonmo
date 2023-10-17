using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytBlacklist
{
    public class HisBhytBlacklistViewFilterQuery : HisBhytBlacklistViewFilter
    {
        public HisBhytBlacklistViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BHYT_BLACKLIST, bool>>> listVHisBhytBlacklistExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BHYT_BLACKLIST, bool>>>();

        

        internal HisBhytBlacklistSO Query()
        {
            HisBhytBlacklistSO search = new HisBhytBlacklistSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBhytBlacklistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBhytBlacklistExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBhytBlacklistExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBhytBlacklistExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBhytBlacklistExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBhytBlacklistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBhytBlacklistExpression.AddRange(listVHisBhytBlacklistExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBhytBlacklistExpression.Clear();
                search.listVHisBhytBlacklistExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
