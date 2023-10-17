using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    public class HisSkinSurgeryDescFilterQuery : HisSkinSurgeryDescFilter
    {
        public HisSkinSurgeryDescFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SKIN_SURGERY_DESC, bool>>> listHisSkinSurgeryDescExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SKIN_SURGERY_DESC, bool>>>();

        

        internal HisSkinSurgeryDescSO Query()
        {
            HisSkinSurgeryDescSO search = new HisSkinSurgeryDescSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSkinSurgeryDescExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSkinSurgeryDescExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisSkinSurgeryDescExpression.AddRange(listHisSkinSurgeryDescExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSkinSurgeryDescExpression.Clear();
                search.listHisSkinSurgeryDescExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
