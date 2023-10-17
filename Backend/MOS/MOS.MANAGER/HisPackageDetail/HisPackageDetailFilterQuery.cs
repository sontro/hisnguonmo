using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackageDetail
{
    public class HisPackageDetailFilterQuery : HisPackageDetailFilter
    {
        public HisPackageDetailFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PACKAGE_DETAIL, bool>>> listHisPackageDetailExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PACKAGE_DETAIL, bool>>>();

        

        internal HisPackageDetailSO Query()
        {
            HisPackageDetailSO search = new HisPackageDetailSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPackageDetailExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPackageDetailExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPackageDetailExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPackageDetailExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisPackageDetailExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.PACKAGE_ID.HasValue)
                {
                    listHisPackageDetailExpression.Add(o => o.PACKAGE_ID == this.PACKAGE_ID.Value);
                }
                if (this.PACKAGE_IDs != null)
                {
                    listHisPackageDetailExpression.Add(o => this.PACKAGE_IDs.Contains(o.PACKAGE_ID));
                }

                search.listHisPackageDetailExpression.AddRange(listHisPackageDetailExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPackageDetailExpression.Clear();
                search.listHisPackageDetailExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
