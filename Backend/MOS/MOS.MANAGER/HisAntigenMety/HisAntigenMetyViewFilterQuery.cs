using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    public class HisAntigenMetyViewFilterQuery : HisAntigenMetyViewFilter
    {
        public HisAntigenMetyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIGEN_METY, bool>>> listVHisAntigenMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIGEN_METY, bool>>>();

        

        internal HisAntigenMetySO Query()
        {
            HisAntigenMetySO search = new HisAntigenMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAntigenMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAntigenMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAntigenMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAntigenMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAntigenMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisAntigenMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisAntigenMetyExpression.AddRange(listVHisAntigenMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAntigenMetyExpression.Clear();
                search.listVHisAntigenMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
