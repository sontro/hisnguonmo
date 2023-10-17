using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHealthExamRank
{
    public class HisHealthExamRankFilterQuery : HisHealthExamRankFilter
    {
        public HisHealthExamRankFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HEALTH_EXAM_RANK, bool>>> listHisHealthExamRankExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HEALTH_EXAM_RANK, bool>>>();

        

        internal HisHealthExamRankSO Query()
        {
            HisHealthExamRankSO search = new HisHealthExamRankSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisHealthExamRankExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHealthExamRankExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHealthExamRankExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHealthExamRankExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHealthExamRankExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisHealthExamRankExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisHealthExamRankExpression.AddRange(listHisHealthExamRankExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHealthExamRankExpression.Clear();
                search.listHisHealthExamRankExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
