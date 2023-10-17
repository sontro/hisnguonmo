using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    public class HisPackageDetailViewFilterQuery : HisPackageDetailViewFilter
    {
        public HisPackageDetailViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PACKAGE_DETAIL, bool>>> listVHisPackageDetailExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PACKAGE_DETAIL, bool>>>();

        

        internal HisPackageDetailSO Query()
        {
            HisPackageDetailSO search = new HisPackageDetailSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPackageDetailExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPackageDetailExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPackageDetailExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPackageDetailExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisPackageDetailExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.PACKAGE_ID.HasValue)
                {
                    listVHisPackageDetailExpression.Add(o => o.PACKAGE_ID == this.PACKAGE_ID.Value);
                }
                if (this.PACKAGE_IDs != null)
                {
                    listVHisPackageDetailExpression.Add(o => this.PACKAGE_IDs.Contains(o.PACKAGE_ID));
                }
                
                search.listVHisPackageDetailExpression.AddRange(listVHisPackageDetailExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPackageDetailExpression.Clear();
                search.listVHisPackageDetailExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
