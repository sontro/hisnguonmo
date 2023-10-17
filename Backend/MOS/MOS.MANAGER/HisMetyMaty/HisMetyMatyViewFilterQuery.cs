using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyMaty
{
    public class HisMetyMatyViewFilterQuery : HisMetyMatyViewFilter
    {
        public HisMetyMatyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_METY_MATY, bool>>> listVHisMetyMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_METY_MATY, bool>>>();



        internal HisMetyMatySO Query()
        {
            HisMetyMatySO search = new HisMetyMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMetyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMetyMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMetyMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMetyMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMetyMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisMetyMatyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listVHisMetyMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.METY_PRODUCT_ID.HasValue)
                {
                    listVHisMetyMatyExpression.Add(o => o.METY_PRODUCT_ID == this.METY_PRODUCT_ID.Value);
                }
                if (this.METY_PRODUCT_IDs != null)
                {
                    listVHisMetyMatyExpression.Add(o => this.METY_PRODUCT_IDs.Contains(o.METY_PRODUCT_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMetyMatyExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisMetyMatyExpression.AddRange(listVHisMetyMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMetyMatyExpression.Clear();
                search.listVHisMetyMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
