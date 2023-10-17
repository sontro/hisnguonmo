using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLicense.Get
{
    public class SdaLicenseFilterQuery : SdaLicenseFilter
    {
        public SdaLicenseFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SDA_LICENSE, bool>>> listExpression = new List<System.Linq.Expressions.Expression<Func<SDA_LICENSE, bool>>>();

        internal OrderProcessorBase OrderProcess = new OrderProcessorBase();

        internal SdaLicenseSO Query()
        {
            SdaLicenseSO search = new SdaLicenseSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null && this.IDs.Count > 0)
                {
                    listExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrEmpty(this.APP_CODE))
                {
                    listExpression.Add(o => o.APP_CODE != null && o.APP_CODE.Equals(this.APP_CODE));
                }

                if (!String.IsNullOrEmpty(this.CLIENT_CODE__EXACT))
                {
                    listExpression.Add(o => o.CLIENT_CODE == this.CLIENT_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.APP_CODE__EXACT))
                {
                    listExpression.Add(o => o.APP_CODE == this.APP_CODE__EXACT);
                }
                if (this.IS_EXPIRED.HasValue)
                {
                    long expiredDate = (Inventec.Common.DateTime.Get.StartDay() ?? 99999999999999) / 1000000;
                    if (this.IS_EXPIRED.Value)
                    {
                        listExpression.Add(o => o.EXPIRED_DATE < expiredDate);
                    }
                    else
                    {
                        listExpression.Add(o => o.EXPIRED_DATE >= expiredDate);
                    }
                }

                search.listSdaLicenseExpression.AddRange(listExpression);
                search.OrderField = OrderProcess.GetOrderField<SDA_LICENSE>(ORDER_FIELD);
                search.OrderDirection = OrderProcess.GetOrderDirection(ORDER_DIRECTION);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSdaLicenseExpression.Clear();
                search.listSdaLicenseExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
