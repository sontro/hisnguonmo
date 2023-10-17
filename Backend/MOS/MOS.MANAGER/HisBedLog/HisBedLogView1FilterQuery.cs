using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    public class HisBedLogView1FilterQuery : HisBedLogView1Filter
    {
        public HisBedLogView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_1, bool>>> listVHisBedLog1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_1, bool>>>();



        internal HisBedLogSO Query()
        {
            HisBedLogSO search = new HisBedLogSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBedLog1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBedLog1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBedLog1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_BED_ROOM_ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.TREATMENT_BED_ROOM_ID == this.TREATMENT_BED_ROOM_ID.Value);
                }
                if (this.TREATMENT_BED_ROOM_IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => this.TREATMENT_BED_ROOM_IDs.Contains(o.TREATMENT_BED_ROOM_ID));
                }
                if (this.BED_ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.BED_ID == this.BED_ID.Value);
                }
                if (this.BED_IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => this.BED_IDs.Contains(o.BED_ID));
                }
                if (this.BED_SERVICE_TYPE_ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.BED_SERVICE_TYPE_ID.HasValue && o.BED_SERVICE_TYPE_ID.Value == this.BED_SERVICE_TYPE_ID.Value);
                }
                if (this.BED_SERVICE_TYPE_IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => o.BED_SERVICE_TYPE_ID.HasValue && this.BED_SERVICE_TYPE_IDs.Contains(o.BED_SERVICE_TYPE_ID.Value));
                }
                if (this.START_TIME_FROM.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.START_TIME >= this.START_TIME_FROM.Value);
                }
                if (this.START_TIME_TO.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.START_TIME <= this.START_TIME_TO.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME >= this.FINISH_TIME_FROM.Value);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME <= this.FINISH_TIME_TO.Value);
                }
                if (this.FINISH_TIME_FROM__OR__NULL.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => !o.FINISH_TIME.HasValue || o.FINISH_TIME.Value >= this.FINISH_TIME_FROM__OR__NULL.Value);
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisBedLog1Expression.Add(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisBedLog1Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.IS_FINISH.HasValue && this.IS_FINISH.Value)
                {
                    listVHisBedLog1Expression.Add(o => o.FINISH_TIME.HasValue);
                }
                if (this.IS_FINISH.HasValue && !this.IS_FINISH.Value)
                {
                    listVHisBedLog1Expression.Add(o => !o.FINISH_TIME.HasValue);
                }
                if (!string.IsNullOrWhiteSpace(this.BED_CODE__OR__BED_NAME))
                {
                    string keyword = this.BED_CODE__OR__BED_NAME.ToLower().Trim();
                    listVHisBedLog1Expression.Add(o =>
                        o.BED_CODE.ToLower().Contains(keyword)
                        || o.BED_NAME.ToLower().Contains(keyword)
                        );
                }

                search.listVHisBedLog1Expression.AddRange(listVHisBedLog1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBedLog1Expression.Clear();
                search.listVHisBedLog1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
