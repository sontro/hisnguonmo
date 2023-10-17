using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamServiceTemp
{
    public class HisExamServiceTempViewFilterQuery : HisExamServiceTempViewFilter
    {
        public HisExamServiceTempViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SERVICE_TEMP, bool>>> listVHisExamServiceTempExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXAM_SERVICE_TEMP, bool>>>();

        

        internal HisExamServiceTempSO Query()
        {
            HisExamServiceTempSO search = new HisExamServiceTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExamServiceTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExamServiceTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExamServiceTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExamServiceTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExamServiceTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisExamServiceTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisExamServiceTempExpression.AddRange(listVHisExamServiceTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExamServiceTempExpression.Clear();
                search.listVHisExamServiceTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
