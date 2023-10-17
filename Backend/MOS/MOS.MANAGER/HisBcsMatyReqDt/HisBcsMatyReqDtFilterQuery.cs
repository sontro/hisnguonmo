using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    public class HisBcsMatyReqDtFilterQuery : HisBcsMatyReqDtFilter
    {
        public HisBcsMatyReqDtFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_DT, bool>>> listHisBcsMatyReqDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BCS_MATY_REQ_DT, bool>>>();

        

        internal HisBcsMatyReqDtSO Query()
        {
            HisBcsMatyReqDtSO search = new HisBcsMatyReqDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisBcsMatyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisBcsMatyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXP_MEST_MATERIAL_ID.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.EXP_MEST_MATERIAL_ID == this.EXP_MEST_MATERIAL_ID.Value);
                }
                if (this.EXP_MEST_MATERIAL_IDs != null)
                {
                    listHisBcsMatyReqDtExpression.Add(o => this.EXP_MEST_MATERIAL_IDs.Contains(o.EXP_MEST_MATERIAL_ID));
                }
                if (this.EXP_MEST_MATY_REQ_ID.HasValue)
                {
                    listHisBcsMatyReqDtExpression.Add(o => o.EXP_MEST_MATY_REQ_ID == this.EXP_MEST_MATY_REQ_ID.Value);
                }
                if (this.EXP_MEST_MATY_REQ_IDs != null)
                {
                    listHisBcsMatyReqDtExpression.Add(o => this.EXP_MEST_MATY_REQ_IDs.Contains(o.EXP_MEST_MATY_REQ_ID));
                }

                search.listHisBcsMatyReqDtExpression.AddRange(listHisBcsMatyReqDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBcsMatyReqDtExpression.Clear();
                search.listHisBcsMatyReqDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
