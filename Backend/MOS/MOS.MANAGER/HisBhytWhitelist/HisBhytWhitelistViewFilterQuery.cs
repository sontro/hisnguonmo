using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytWhitelist
{
    public class HisBhytWhitelistViewFilterQuery : HisBhytWhitelistViewFilter
    {
        public HisBhytWhitelistViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BHYT_WHITELIST, bool>>> listVHisBhytWhitelistExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BHYT_WHITELIST, bool>>>();

        

        internal HisBhytWhitelistSO Query()
        {
            HisBhytWhitelistSO search = new HisBhytWhitelistSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBhytWhitelistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBhytWhitelistExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBhytWhitelistExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBhytWhitelistExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBhytWhitelistExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBhytWhitelistExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisBhytWhitelistExpression.AddRange(listVHisBhytWhitelistExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBhytWhitelistExpression.Clear();
                search.listVHisBhytWhitelistExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
