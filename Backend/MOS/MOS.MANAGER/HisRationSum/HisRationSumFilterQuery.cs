using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSum
{
    public class HisRationSumFilterQuery : HisRationSumFilter
    {
        public HisRationSumFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM, bool>>> listHisRationSumExpression = new List<System.Linq.Expressions.Expression<Func<HIS_RATION_SUM, bool>>>();



        internal HisRationSumSO Query()
        {
            HisRationSumSO search = new HisRationSumSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisRationSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisRationSumExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisRationSumExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisRationSumExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisRationSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisRationSumExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listHisRationSumExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.RATION_SUM_STT_ID.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.RATION_SUM_STT_ID == this.RATION_SUM_STT_ID.Value);
                }
                if (this.RATION_SUM_STT_IDs != null)
                {
                    listHisRationSumExpression.Add(o => this.RATION_SUM_STT_IDs.Contains(o.RATION_SUM_STT_ID));
                }
                if (this.REQ_DATE_FROM.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.REQ_DATE.HasValue && o.REQ_DATE.Value >= this.REQ_DATE_FROM.Value);
                }
                if (this.REQ_DATE_TO.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.REQ_DATE.HasValue && o.REQ_DATE.Value <= this.REQ_DATE_TO.Value);
                }
                if (this.APPROVAL_DATE_FROM.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.APPROVAL_DATE.HasValue && o.APPROVAL_DATE.Value >= this.APPROVAL_DATE_FROM.Value);
                }
                if (this.APPROVAL_DATE_TO.HasValue)
                {
                    listHisRationSumExpression.Add(o => o.APPROVAL_DATE.HasValue && o.APPROVAL_DATE.Value <= this.APPROVAL_DATE_TO.Value);
                }

                search.listHisRationSumExpression.AddRange(listHisRationSumExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisRationSumExpression.Clear();
                search.listHisRationSumExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
