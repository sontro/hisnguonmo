using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    public class HisBcsMetyReqDtFilterQuery : HisBcsMetyReqDtFilter
    {
        public HisBcsMetyReqDtFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BCS_METY_REQ_DT, bool>>> listHisBcsMetyReqDtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BCS_METY_REQ_DT, bool>>>();



        internal HisBcsMetyReqDtSO Query()
        {
            HisBcsMetyReqDtSO search = new HisBcsMetyReqDtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisBcsMetyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisBcsMetyReqDtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.EXP_MEST_MEDICINE_ID.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.EXP_MEST_MEDICINE_ID == this.EXP_MEST_MEDICINE_ID.Value);
                }
                if (this.EXP_MEST_MEDICINE_IDs != null)
                {
                    listHisBcsMetyReqDtExpression.Add(o => this.EXP_MEST_MEDICINE_IDs.Contains(o.EXP_MEST_MEDICINE_ID));
                }

                if (this.EXP_MEST_METY_REQ_ID.HasValue)
                {
                    listHisBcsMetyReqDtExpression.Add(o => o.EXP_MEST_METY_REQ_ID == this.EXP_MEST_METY_REQ_ID.Value);
                }
                if (this.EXP_MEST_METY_REQ_IDs != null)
                {
                    listHisBcsMetyReqDtExpression.Add(o => this.EXP_MEST_METY_REQ_IDs.Contains(o.EXP_MEST_METY_REQ_ID));
                }

                search.listHisBcsMetyReqDtExpression.AddRange(listHisBcsMetyReqDtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBcsMetyReqDtExpression.Clear();
                search.listHisBcsMetyReqDtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
