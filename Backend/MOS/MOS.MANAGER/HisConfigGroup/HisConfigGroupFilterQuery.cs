using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    public class HisConfigGroupFilterQuery : HisConfigGroupFilter
    {
        public HisConfigGroupFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CONFIG_GROUP, bool>>> listHisConfigGroupExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONFIG_GROUP, bool>>>();

        

        internal HisConfigGroupSO Query()
        {
            HisConfigGroupSO search = new HisConfigGroupSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisConfigGroupExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisConfigGroupExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisConfigGroupExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisConfigGroupExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisConfigGroupExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisConfigGroupExpression.Add(o =>
                        (o.CONFIG_GROUP_CODE != null && o.CONFIG_GROUP_CODE.ToLower().Contains(this.KEY_WORD)) ||
                        (o.CONFIG_GROUP_NAME != null && o.CONFIG_GROUP_NAME.ToLower().Contains(this.KEY_WORD)) ||
                        (o.CREATOR != null && o.CREATOR.ToLower().Contains(this.KEY_WORD)) ||
                        (o.MODIFIER != null && o.MODIFIER.ToLower().Contains(this.KEY_WORD))
                        );
                }

                search.listHisConfigGroupExpression.AddRange(listHisConfigGroupExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisConfigGroupExpression.Clear();
                search.listHisConfigGroupExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
