using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroup
{
    public class HisPtttGroupViewFilterQuery : HisPtttGroupViewFilter
    {
        public HisPtttGroupViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP, bool>>> listVHisPtttGroupExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP, bool>>>();

        

        internal HisPtttGroupSO Query()
        {
            HisPtttGroupSO search = new HisPtttGroupSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPtttGroupExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPtttGroupExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPtttGroupExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPtttGroupExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPtttGroupExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.PTTT_GROUP_CODE__EXACT))
                {
                    listVHisPtttGroupExpression.Add(o => o.PTTT_GROUP_CODE == this.PTTT_GROUP_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisPtttGroupExpression.Add(o =>
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PTTT_GROUP_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisPtttGroupExpression.AddRange(listVHisPtttGroupExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPtttGroupExpression.Clear();
                search.listVHisPtttGroupExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
