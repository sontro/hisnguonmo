using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    public class HisSevereIllnessInfoViewFilterQuery : HisSevereIllnessInfoViewFilter
    {
        public HisSevereIllnessInfoViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SEVERE_ILLNESS_INFO, bool>>> listVHisSevereIllnessInfoExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SEVERE_ILLNESS_INFO, bool>>>();

        

        internal HisSevereIllnessInfoSO Query()
        {
            HisSevereIllnessInfoSO search = new HisSevereIllnessInfoSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSevereIllnessInfoExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisSevereIllnessInfoExpression.AddRange(listVHisSevereIllnessInfoExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSevereIllnessInfoExpression.Clear();
                search.listVHisSevereIllnessInfoExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
