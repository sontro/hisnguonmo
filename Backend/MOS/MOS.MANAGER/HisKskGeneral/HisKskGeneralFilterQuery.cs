using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskGeneral
{
    public class HisKskGeneralFilterQuery : HisKskGeneralFilter
    {
        public HisKskGeneralFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_GENERAL, bool>>> listHisKskGeneralExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_GENERAL, bool>>>();



        internal HisKskGeneralSO Query()
        {
            HisKskGeneralSO search = new HisKskGeneralSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisKskGeneralExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskGeneralExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskGeneralExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskGeneralExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisKskGeneralExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listHisKskGeneralExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }

                search.listHisKskGeneralExpression.AddRange(listHisKskGeneralExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskGeneralExpression.Clear();
                search.listHisKskGeneralExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
