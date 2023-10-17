using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    public class HisTransfusionFilterQuery : HisTransfusionFilter
    {
        public HisTransfusionFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION, bool>>> listHisTransfusionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION, bool>>>();



        internal HisTransfusionSO Query()
        {
            HisTransfusionSO search = new HisTransfusionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisTransfusionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTransfusionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTransfusionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTransfusionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTransfusionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TRANSFUSION_SUM_ID.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.TRANSFUSION_SUM_ID == this.TRANSFUSION_SUM_ID.Value);
                }
                if (this.TRANSFUSION_SUM_IDs != null)
                {
                    listHisTransfusionExpression.Add(o => this.TRANSFUSION_SUM_IDs.Contains(o.TRANSFUSION_SUM_ID));
                }

                if (this.MEASURE_TIME_FROM.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.MEASURE_TIME >= this.MEASURE_TIME_FROM.Value);
                }
                if (this.MEASURE_TIME_TO.HasValue)
                {
                    listHisTransfusionExpression.Add(o => o.MEASURE_TIME <= this.MEASURE_TIME_TO.Value);
                }

                search.listHisTransfusionExpression.AddRange(listHisTransfusionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTransfusionExpression.Clear();
                search.listHisTransfusionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
