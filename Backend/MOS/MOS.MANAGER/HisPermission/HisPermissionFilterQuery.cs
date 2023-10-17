using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    public class HisPermissionFilterQuery : HisPermissionFilter
    {
        public HisPermissionFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PERMISSION, bool>>> listHisPermissionExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PERMISSION, bool>>>();



        internal HisPermissionSO Query()
        {
            HisPermissionSO search = new HisPermissionSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisPermissionExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPermissionExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPermissionExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPermissionExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.PERMISSION_TYPE_ID.HasValue)
                {
                    listHisPermissionExpression.Add(o => o.PERMISSION_TYPE_ID == this.PERMISSION_TYPE_ID.Value);
                }
                if (this.PERMISSION_TYPE_IDs != null)
                {
                    listHisPermissionExpression.Add(o => this.PERMISSION_TYPE_IDs.Contains(o.PERMISSION_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisPermissionExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisPermissionExpression.AddRange(listHisPermissionExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPermissionExpression.Clear();
                search.listHisPermissionExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
