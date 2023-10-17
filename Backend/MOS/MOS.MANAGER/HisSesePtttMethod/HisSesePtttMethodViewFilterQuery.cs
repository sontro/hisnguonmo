using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    public class HisSesePtttMethodViewFilterQuery : HisSesePtttMethodViewFilter
    {
        public HisSesePtttMethodViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SESE_PTTT_METHOD, bool>>> listVHisSesePtttMethodExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SESE_PTTT_METHOD, bool>>>();

        

        internal HisSesePtttMethodSO Query()
        {
            HisSesePtttMethodSO search = new HisSesePtttMethodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSesePtttMethodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSesePtttMethodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSesePtttMethodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSesePtttMethodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSesePtttMethodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisSesePtttMethodExpression.AddRange(listVHisSesePtttMethodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSesePtttMethodExpression.Clear();
                search.listVHisSesePtttMethodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
