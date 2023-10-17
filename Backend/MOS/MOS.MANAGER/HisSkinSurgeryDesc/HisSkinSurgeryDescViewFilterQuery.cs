using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    public class HisSkinSurgeryDescViewFilterQuery : HisSkinSurgeryDescViewFilter
    {
        public HisSkinSurgeryDescViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SKIN_SURGERY_DESC, bool>>> listVHisSkinSurgeryDescExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SKIN_SURGERY_DESC, bool>>>();

        

        internal HisSkinSurgeryDescSO Query()
        {
            HisSkinSurgeryDescSO search = new HisSkinSurgeryDescSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSkinSurgeryDescExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisSkinSurgeryDescExpression.AddRange(listVHisSkinSurgeryDescExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSkinSurgeryDescExpression.Clear();
                search.listVHisSkinSurgeryDescExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
