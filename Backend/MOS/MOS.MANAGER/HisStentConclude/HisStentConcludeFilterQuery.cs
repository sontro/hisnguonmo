using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    public class HisStentConcludeFilterQuery : HisStentConcludeFilter
    {
        public HisStentConcludeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_STENT_CONCLUDE, bool>>> listHisStentConcludeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_STENT_CONCLUDE, bool>>>();

        

        internal HisStentConcludeSO Query()
        {
            HisStentConcludeSO search = new HisStentConcludeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisStentConcludeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisStentConcludeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisStentConcludeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisStentConcludeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.SERE_SERV_IDs != null)
                {
                    listHisStentConcludeExpression.Add(o =>o.SERE_SERV_ID.HasValue && this.SERE_SERV_IDs.Contains(o.SERE_SERV_ID.Value));
                }
                if (this.SERE_SERV_ID.HasValue)
                {
                    listHisStentConcludeExpression.Add(o => o.SERE_SERV_ID == this.SERE_SERV_ID.Value);
                }
                search.listHisStentConcludeExpression.AddRange(listHisStentConcludeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisStentConcludeExpression.Clear();
                search.listHisStentConcludeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
