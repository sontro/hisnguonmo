using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMety
{
    public class HisMetyMetyFilterQuery : HisMetyMetyFilter
    {
        public HisMetyMetyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_METY_METY, bool>>> listHisMetyMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_METY_METY, bool>>>();



        internal HisMetyMetySO Query()
        {
            HisMetyMetySO search = new HisMetyMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMetyMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMetyMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMetyMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMetyMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMetyMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.METY_PRODUCT_ID.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.METY_PRODUCT_ID == this.METY_PRODUCT_ID.Value);
                }
                if (this.METY_PRODUCT_IDs != null)
                {
                    listHisMetyMetyExpression.Add(o => this.METY_PRODUCT_IDs.Contains(o.METY_PRODUCT_ID));
                }
                if (this.PREPARATION_MEDICINE_TYPE_ID.HasValue)
                {
                    listHisMetyMetyExpression.Add(o => o.PREPARATION_MEDICINE_TYPE_ID == this.PREPARATION_MEDICINE_TYPE_ID.Value);
                }
                if (this.PREPARATION_MEDICINE_TYPE_IDs != null)
                {
                    listHisMetyMetyExpression.Add(o => this.PREPARATION_MEDICINE_TYPE_IDs.Contains(o.PREPARATION_MEDICINE_TYPE_ID));
                }

                search.listHisMetyMetyExpression.AddRange(listHisMetyMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMetyMetyExpression.Clear();
                search.listHisMetyMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
