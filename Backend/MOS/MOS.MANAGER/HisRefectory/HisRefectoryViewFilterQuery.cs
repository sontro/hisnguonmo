using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    public class HisRefectoryViewFilterQuery : HisRefectoryViewFilter
    {
        public HisRefectoryViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_REFECTORY, bool>>> listVHisRefectoryExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_REFECTORY, bool>>>();



        internal HisRefectorySO Query()
        {
            HisRefectorySO search = new HisRefectorySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisRefectoryExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisRefectoryExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisRefectoryExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisRefectoryExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisRefectoryExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisRefectoryExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisRefectoryExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.ROOM_TYPE_ID.HasValue)
                {
                    listVHisRefectoryExpression.Add(o => o.ROOM_TYPE_ID == this.ROOM_TYPE_ID.Value);
                }
                if (this.ROOM_TYPE_IDs != null)
                {
                    listVHisRefectoryExpression.Add(o => this.ROOM_TYPE_IDs.Contains(o.ROOM_TYPE_ID));
                }


                if (!String.IsNullOrWhiteSpace(this.REFECTORY_CODE__EXACT))
                {
                    listVHisRefectoryExpression.Add(o => o.REFECTORY_CODE == this.REFECTORY_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.DEPARTMENT_CODE__EXACT))
                {
                    listVHisRefectoryExpression.Add(o => o.DEPARTMENT_CODE == this.DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.BRANCH_CODE__EXACT))
                {
                    listVHisRefectoryExpression.Add(o => o.BRANCH_CODE == this.BRANCH_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisRefectoryExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.REFECTORY_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REFECTORY_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.BRANCH_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.BRANCH_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.G_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisRefectoryExpression.AddRange(listVHisRefectoryExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisRefectoryExpression.Clear();
                search.listVHisRefectoryExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
