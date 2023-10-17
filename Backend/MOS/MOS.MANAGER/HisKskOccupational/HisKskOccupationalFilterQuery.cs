using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOccupational
{
    public class HisKskOccupationalFilterQuery : HisKskOccupationalFilter
    {
        public HisKskOccupationalFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_OCCUPATIONAL, bool>>> listHisKskOccupationalExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_OCCUPATIONAL, bool>>>();



        internal HisKskOccupationalSO Query()
        {
            HisKskOccupationalSO search = new HisKskOccupationalSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisKskOccupationalExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskOccupationalExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskOccupationalExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskOccupationalExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisKskOccupationalExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listHisKskOccupationalExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }

                search.listHisKskOccupationalExpression.AddRange(listHisKskOccupationalExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskOccupationalExpression.Clear();
                search.listHisKskOccupationalExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
