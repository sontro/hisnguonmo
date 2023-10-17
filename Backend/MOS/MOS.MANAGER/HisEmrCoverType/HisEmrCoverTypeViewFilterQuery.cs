using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverType
{
    public class HisEmrCoverTypeViewFilterQuery : HisEmrCoverTypeViewFilter
    {
        public HisEmrCoverTypeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_TYPE, bool>>> listVHisEmrCoverTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_TYPE, bool>>>();

        

        internal HisEmrCoverTypeSO Query()
        {
            HisEmrCoverTypeSO search = new HisEmrCoverTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisEmrCoverTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEmrCoverTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisEmrCoverTypeExpression.AddRange(listVHisEmrCoverTypeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEmrCoverTypeExpression.Clear();
                search.listVHisEmrCoverTypeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
