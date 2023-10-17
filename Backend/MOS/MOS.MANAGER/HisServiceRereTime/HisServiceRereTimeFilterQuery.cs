using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRereTime
{
    public class HisServiceRereTimeFilterQuery : HisServiceRereTimeFilter
    {
        public HisServiceRereTimeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RERE_TIME, bool>>> listHisServiceRereTimeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RERE_TIME, bool>>>();

        

        internal HisServiceRereTimeSO Query()
        {
            HisServiceRereTimeSO search = new HisServiceRereTimeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisServiceRereTimeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceRereTimeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceRereTimeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceRereTimeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisServiceRereTimeExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisServiceRereTimeExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                search.listHisServiceRereTimeExpression.AddRange(listHisServiceRereTimeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceRereTimeExpression.Clear();
                search.listHisServiceRereTimeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
