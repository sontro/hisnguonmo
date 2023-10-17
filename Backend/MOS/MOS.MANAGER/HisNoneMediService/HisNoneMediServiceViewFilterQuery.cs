using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    public class HisNoneMediServiceViewFilterQuery : HisNoneMediServiceViewFilter
    {
        public HisNoneMediServiceViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_NONE_MEDI_SERVICE, bool>>> listVHisNoneMediServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_NONE_MEDI_SERVICE, bool>>>();

        

        internal HisNoneMediServiceSO Query()
        {
            HisNoneMediServiceSO search = new HisNoneMediServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisNoneMediServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisNoneMediServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisNoneMediServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisNoneMediServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisNoneMediServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisNoneMediServiceExpression.Add(o =>
                        o.NONE_MEDI_SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.NONE_MEDI_SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisNoneMediServiceExpression.AddRange(listVHisNoneMediServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisNoneMediServiceExpression.Clear();
                search.listVHisNoneMediServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
