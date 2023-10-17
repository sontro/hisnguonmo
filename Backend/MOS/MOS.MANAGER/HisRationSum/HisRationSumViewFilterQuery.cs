using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSum
{
    public class HisRationSumViewFilterQuery : HisRationSumViewFilter
    {
        public HisRationSumViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SUM, bool>>> listVHisRationSumExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_RATION_SUM, bool>>>();



        internal HisRationSumSO Query()
        {
            HisRationSumSO search = new HisRationSumSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisRationSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisRationSumExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisRationSumExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisRationSumExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisRationSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisRationSumExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisRationSumExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.DEPARTMENT_CODE__EXACT))
                {
                    listVHisRationSumExpression.Add(o => o.DEPARTMENT_CODE == this.DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.ROOM_CODE__EXACT))
                {
                    listVHisRationSumExpression.Add(o => o.ROOM_CODE == this.ROOM_CODE__EXACT);
                }
                if (this.RATION_SUM_STT_ID.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.RATION_SUM_STT_ID == this.RATION_SUM_STT_ID.Value);
                }
                if (this.RATION_SUM_STT_IDs != null)
                {
                    listVHisRationSumExpression.Add(o => this.RATION_SUM_STT_IDs.Contains(o.RATION_SUM_STT_ID));
                }
                if (this.REQ_DATE_FROM.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.REQ_DATE.HasValue && o.REQ_DATE.Value >= this.REQ_DATE_FROM.Value);
                }
                if (this.REQ_DATE_TO.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.REQ_DATE.HasValue && o.REQ_DATE.Value <= this.REQ_DATE_TO.Value);
                }
                if (this.APPROVAL_DATE_FROM.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.APPROVAL_DATE.HasValue && o.APPROVAL_DATE.Value >= this.APPROVAL_DATE_FROM.Value);
                }
                if (this.APPROVAL_DATE_TO.HasValue)
                {
                    listVHisRationSumExpression.Add(o => o.APPROVAL_DATE.HasValue && o.APPROVAL_DATE.Value <= this.APPROVAL_DATE_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisRationSumExpression.Add(o => o.APPROVAL_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.APPROVAL_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.RATION_SUM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.RATION_SUM_STT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.RATION_SUM_STT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQ_DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQ_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQ_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQ_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisRationSumExpression.AddRange(listVHisRationSumExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRationSumExpression.Clear();
                search.listVHisRationSumExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
