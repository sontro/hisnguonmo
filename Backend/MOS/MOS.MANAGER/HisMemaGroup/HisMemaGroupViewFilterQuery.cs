using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMemaGroup
{
    public class HisMemaGroupViewFilterQuery : HisMemaGroupViewFilter
    {
        public HisMemaGroupViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEMA_GROUP, bool>>> listVHisMemaGroupExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEMA_GROUP, bool>>>();

        

        internal HisMemaGroupSO Query()
        {
            HisMemaGroupSO search = new HisMemaGroupSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMemaGroupExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMemaGroupExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMemaGroupExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMemaGroupExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMemaGroupExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMemaGroupExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisMemaGroupExpression.AddRange(listVHisMemaGroupExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMemaGroupExpression.Clear();
                search.listVHisMemaGroupExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
