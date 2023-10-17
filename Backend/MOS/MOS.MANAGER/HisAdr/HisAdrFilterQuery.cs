using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdr
{
    public class HisAdrFilterQuery : HisAdrFilter
    {
        public HisAdrFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ADR, bool>>> listHisAdrExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ADR, bool>>>();

        

        internal HisAdrSO Query()
        {
            HisAdrSO search = new HisAdrSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAdrExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAdrExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAdrExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAdrExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAdrExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAdrExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAdrExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAdrExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAdrExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAdrExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisAdrExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisAdrExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisAdrExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }

                search.listHisAdrExpression.AddRange(listHisAdrExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAdrExpression.Clear();
                search.listHisAdrExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
