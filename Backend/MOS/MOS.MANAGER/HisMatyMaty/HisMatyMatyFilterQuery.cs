using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    public class HisMatyMatyFilterQuery : HisMatyMatyFilter
    {
        public HisMatyMatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MATY_MATY, bool>>> listHisMatyMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MATY_MATY, bool>>>();

        

        internal HisMatyMatySO Query()
        {
            HisMatyMatySO search = new HisMatyMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMatyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMatyMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMatyMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMatyMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMatyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMatyMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                if (this.PREPARATION_MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMatyMatyExpression.Add(o => o.PREPARATION_MATERIAL_TYPE_ID == this.PREPARATION_MATERIAL_TYPE_ID.Value);
                }
                if (this.PREPARATION_MATERIAL_TYPE_IDs != null)
                {
                    listHisMatyMatyExpression.Add(o => this.PREPARATION_MATERIAL_TYPE_IDs.Contains(o.PREPARATION_MATERIAL_TYPE_ID));
                }

                search.listHisMatyMatyExpression.AddRange(listHisMatyMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMatyMatyExpression.Clear();
                search.listHisMatyMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
