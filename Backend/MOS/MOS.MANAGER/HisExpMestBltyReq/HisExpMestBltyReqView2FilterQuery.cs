using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    public class HisExpMestBltyReqView2FilterQuery : HisExpMestBltyReqView2Filter
    {
        public HisExpMestBltyReqView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_BLTY_REQ_2, bool>>> listVHisExpMestBltyReq2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_BLTY_REQ_2, bool>>>();



        internal HisExpMestBltyReqSO Query()
        {
            HisExpMestBltyReqSO search = new HisExpMestBltyReqSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXP_MEST_ID.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.EXP_MEST_ID == this.EXP_MEST_ID.Value);
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID));
                }
                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.TRACKING_ID.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.TRACKING_ID == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listVHisExpMestBltyReq2Expression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID));
                }

                search.listVHisExpMestBltyReq2Expression.AddRange(listVHisExpMestBltyReq2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestBltyReq1Expression.Clear();
                search.listVHisExpMestBltyReq1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
