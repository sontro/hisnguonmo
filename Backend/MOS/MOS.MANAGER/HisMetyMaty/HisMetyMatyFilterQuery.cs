using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMaty
{
    public class HisMetyMatyFilterQuery : HisMetyMatyFilter
    {
        public HisMetyMatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_METY_MATY, bool>>> listHisMetyMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_METY_MATY, bool>>>();



        internal HisMetyMatySO Query()
        {
            HisMetyMatySO search = new HisMetyMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMetyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMetyMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMetyMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMetyMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMetyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.METY_PRODUCT_ID.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.METY_PRODUCT_ID == this.METY_PRODUCT_ID.Value);
                }
                if (this.METY_PRODUCT_IDs != null)
                {
                    listHisMetyMatyExpression.Add(o => this.METY_PRODUCT_IDs.Contains(o.METY_PRODUCT_ID));
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMetyMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMetyMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                search.listHisMetyMatyExpression.AddRange(listHisMetyMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMetyMatyExpression.Clear();
                search.listHisMetyMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
