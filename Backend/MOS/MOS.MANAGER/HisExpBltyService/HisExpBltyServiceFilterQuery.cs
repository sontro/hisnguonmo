using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpBltyService
{
    public class HisExpBltyServiceFilterQuery : HisExpBltyServiceFilter
    {
        public HisExpBltyServiceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXP_BLTY_SERVICE, bool>>> listHisExpBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXP_BLTY_SERVICE, bool>>>();

        

        internal HisExpBltyServiceSO Query()
        {
            HisExpBltyServiceSO search = new HisExpBltyServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisExpBltyServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisExpBltyServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisExpBltyServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisExpBltyServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listHisExpBltyServiceExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.EXP_MEST_ID.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.EXP_MEST_ID == this.EXP_MEST_ID.Value);
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listHisExpBltyServiceExpression.Add(o => this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listHisExpBltyServiceExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisExpBltyServiceExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                search.listHisExpBltyServiceExpression.AddRange(listHisExpBltyServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExpBltyServiceExpression.Clear();
                search.listHisExpBltyServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
