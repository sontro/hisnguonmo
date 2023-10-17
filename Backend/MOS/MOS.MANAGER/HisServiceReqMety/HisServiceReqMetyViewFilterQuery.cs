using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMety
{
    public class HisServiceReqMetyViewFilterQuery : HisServiceReqMetyViewFilter
    {
        public HisServiceReqMetyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_METY, bool>>> listVHisServiceReqMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_REQ_METY, bool>>>();



        internal HisServiceReqMetySO Query()
        {
            HisServiceReqMetySO search = new HisServiceReqMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceReqMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceReqMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceReqMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.SERVICE_REQ_ID.Value == o.SERVICE_REQ_ID);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.SERVICE_REQ_ID__NOT_INs != null)
                {
                    listVHisServiceReqMetyExpression.Add(o => !this.SERVICE_REQ_ID__NOT_INs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue && this.MEDICINE_TYPE_ID.Value == o.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue && this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID.Value));
                }
                if (this.INTRUCTION_DATE_FROM.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.INTRUCTION_DATE >= this.INTRUCTION_DATE_FROM.Value);
                }
                if (this.INTRUCTION_DATE_TO.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.INTRUCTION_DATE <= this.INTRUCTION_DATE_TO.Value);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.TREATMENT_ID.Value == o.TREATMENT_ID);
                }
                if (this.INTRUCTION_DATE__EQUAL.HasValue)
                {
                    listVHisServiceReqMetyExpression.Add(o => o.INTRUCTION_DATE == this.INTRUCTION_DATE__EQUAL.Value);
                }
                if (this.TREATMENT_IDs != null && this.TREATMENT_IDs.Count > 0)
                {
                    listVHisServiceReqMetyExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                search.listVHisServiceReqMetyExpression.AddRange(listVHisServiceReqMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceReqMetyExpression.Clear();
                search.listVHisServiceReqMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
