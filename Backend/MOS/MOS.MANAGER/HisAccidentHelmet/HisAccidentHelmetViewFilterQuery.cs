using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHelmet
{
    public class HisAccidentHelmetViewFilterQuery : HisAccidentHelmetViewFilter
    {
        public HisAccidentHelmetViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ACCIDENT_HELMET, bool>>> listVHisAccidentHelmetExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ACCIDENT_HELMET, bool>>>();

        

        internal HisAccidentHelmetSO Query()
        {
            HisAccidentHelmetSO search = new HisAccidentHelmetSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAccidentHelmetExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAccidentHelmetExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAccidentHelmetExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAccidentHelmetExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAccidentHelmetExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisAccidentHelmetExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisAccidentHelmetExpression.AddRange(listVHisAccidentHelmetExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAccidentHelmetExpression.Clear();
                search.listVHisAccidentHelmetExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
