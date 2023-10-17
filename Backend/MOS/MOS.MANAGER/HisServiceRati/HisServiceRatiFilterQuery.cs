using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRati
{
    public class HisServiceRatiFilterQuery : HisServiceRatiFilter
    {
        public HisServiceRatiFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RATI, bool>>> listHisServiceRatiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERVICE_RATI, bool>>>();

        

        internal HisServiceRatiSO Query()
        {
            HisServiceRatiSO search = new HisServiceRatiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisServiceRatiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisServiceRatiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisServiceRatiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisServiceRatiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisServiceRatiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisServiceRatiExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.RATION_TIME_ID.HasValue)
                {
                    listHisServiceRatiExpression.Add(o => o.RATION_TIME_ID == this.RATION_TIME_ID.Value);
                }
                if (this.RATION_TIME_IDs != null)
                {
                    listHisServiceRatiExpression.Add(o => this.RATION_TIME_IDs.Contains(o.RATION_TIME_ID));
                }

                search.listHisServiceRatiExpression.AddRange(listHisServiceRatiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisServiceRatiExpression.Clear();
                search.listHisServiceRatiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
