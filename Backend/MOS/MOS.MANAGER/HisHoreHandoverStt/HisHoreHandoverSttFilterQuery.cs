using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    public class HisHoreHandoverSttFilterQuery : HisHoreHandoverSttFilter
    {
        public HisHoreHandoverSttFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER_STT, bool>>> listHisHoreHandoverSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HORE_HANDOVER_STT, bool>>>();

        

        internal HisHoreHandoverSttSO Query()
        {
            HisHoreHandoverSttSO search = new HisHoreHandoverSttSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisHoreHandoverSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHoreHandoverSttExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHoreHandoverSttExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHoreHandoverSttExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHoreHandoverSttExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisHoreHandoverSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisHoreHandoverSttExpression.AddRange(listHisHoreHandoverSttExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHoreHandoverSttExpression.Clear();
                search.listHisHoreHandoverSttExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
