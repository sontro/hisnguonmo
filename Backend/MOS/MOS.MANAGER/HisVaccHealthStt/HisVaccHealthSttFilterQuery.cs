using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccHealthStt
{
    public class HisVaccHealthSttFilterQuery : HisVaccHealthSttFilter
    {
        public HisVaccHealthSttFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VACC_HEALTH_STT, bool>>> listHisVaccHealthSttExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VACC_HEALTH_STT, bool>>>();

        

        internal HisVaccHealthSttSO Query()
        {
            HisVaccHealthSttSO search = new HisVaccHealthSttSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisVaccHealthSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVaccHealthSttExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVaccHealthSttExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVaccHealthSttExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVaccHealthSttExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisVaccHealthSttExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisVaccHealthSttExpression.AddRange(listHisVaccHealthSttExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVaccHealthSttExpression.Clear();
                search.listHisVaccHealthSttExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
