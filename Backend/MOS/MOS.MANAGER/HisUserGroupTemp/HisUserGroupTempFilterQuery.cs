using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    public class HisUserGroupTempFilterQuery : HisUserGroupTempFilter
    {
        public HisUserGroupTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP, bool>>> listHisUserGroupTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_USER_GROUP_TEMP, bool>>>();

        

        internal HisUserGroupTempSO Query()
        {
            HisUserGroupTempSO search = new HisUserGroupTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisUserGroupTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisUserGroupTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisUserGroupTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisUserGroupTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisUserGroupTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisUserGroupTempExpression.AddRange(listHisUserGroupTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisUserGroupTempExpression.Clear();
                search.listHisUserGroupTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
