using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisArea
{
    public class HisAreaFilterQuery : HisAreaFilter
    {
        public HisAreaFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_AREA, bool>>> listHisAreaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_AREA, bool>>>();



        internal HisAreaSO Query()
        {
            HisAreaSO search = new HisAreaSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAreaExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisAreaExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAreaExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAreaExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAreaExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAreaExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAreaExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAreaExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAreaExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAreaExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisAreaExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listHisAreaExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID.Value));
                }
                if (!String.IsNullOrWhiteSpace(this.AREA_CODE__EXACT))
                {
                    listHisAreaExpression.Add(o => o.AREA_CODE == this.AREA_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisAreaExpression.Add(o => o.AREA_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.AREA_NAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisAreaExpression.AddRange(listHisAreaExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAreaExpression.Clear();
                search.listHisAreaExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
