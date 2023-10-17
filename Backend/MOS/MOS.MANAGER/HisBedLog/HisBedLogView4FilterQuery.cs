using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    public class HisBedLogView4FilterQuery : HisBedLogView4Filter
    {
        public HisBedLogView4FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_4, bool>>> listVHisBedLog4Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BED_LOG_4, bool>>>();



        internal HisBedLogSO Query()
        {
            HisBedLogSO search = new HisBedLogSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBedLog4Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBedLog4Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBedLog4Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_BED_ROOM_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.TREATMENT_BED_ROOM_ID == this.TREATMENT_BED_ROOM_ID.Value);
                }
                if (this.TREATMENT_BED_ROOM_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.TREATMENT_BED_ROOM_IDs.Contains(o.TREATMENT_BED_ROOM_ID));
                }
                if (this.BED_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.BED_ID == this.BED_ID.Value);
                }
                if (this.BED_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.BED_IDs.Contains(o.BED_ID));
                }
                if (this.BED_SERVICE_TYPE_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.BED_SERVICE_TYPE_ID.HasValue && o.BED_SERVICE_TYPE_ID.Value == this.BED_SERVICE_TYPE_ID.Value);
                }
                if (this.BED_SERVICE_TYPE_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => o.BED_SERVICE_TYPE_ID.HasValue && this.BED_SERVICE_TYPE_IDs.Contains(o.BED_SERVICE_TYPE_ID.Value));
                }
                if (this.START_TIME_FROM.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.START_TIME >= this.START_TIME_FROM.Value);
                }
                if (this.START_TIME_TO.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.START_TIME <= this.START_TIME_TO.Value);
                }
                if (this.FINISH_TIME_FROM.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME >= this.FINISH_TIME_FROM.Value);
                }
                if (this.FINISH_TIME_TO.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.FINISH_TIME.HasValue && o.FINISH_TIME <= this.FINISH_TIME_TO.Value);
                }
                if (this.FINISH_TIME_FROM__OR__NULL.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => !o.FINISH_TIME.HasValue || o.FINISH_TIME.Value >= this.FINISH_TIME_FROM__OR__NULL.Value);
                }
                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.BED_ROOM_ID.HasValue)
                {
                    listVHisBedLog4Expression.Add(o => o.BED_ROOM_ID == this.BED_ROOM_ID.Value);
                }
                if (this.BED_ROOM_IDs != null)
                {
                    listVHisBedLog4Expression.Add(o => this.BED_ROOM_IDs.Contains(o.BED_ROOM_ID));
                }

                search.listVHisBedLog4Expression.AddRange(listVHisBedLog4Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBedLog4Expression.Clear();
                search.listVHisBedLog4Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
