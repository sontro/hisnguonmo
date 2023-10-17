using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    public class HisBltyServiceFilterQuery : HisBltyServiceFilter
    {
        public HisBltyServiceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BLTY_SERVICE, bool>>> listHisBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BLTY_SERVICE, bool>>>();



        internal HisBltyServiceSO Query()
        {
            HisBltyServiceSO search = new HisBltyServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisBltyServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBltyServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBltyServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBltyServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listHisBltyServiceExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listHisBltyServiceExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisBltyServiceExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                search.listHisBltyServiceExpression.AddRange(listHisBltyServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBltyServiceExpression.Clear();
                search.listHisBltyServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
