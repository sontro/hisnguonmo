using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMaty
{
    public class HisAnticipateMatyViewFilterQuery : HisAnticipateMatyViewFilter
    {
        public HisAnticipateMatyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTICIPATE_MATY, bool>>> listVHisAnticipateMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTICIPATE_MATY, bool>>>();

        

        internal HisAnticipateMatySO Query()
        {
            HisAnticipateMatySO search = new HisAnticipateMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAnticipateMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAnticipateMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAnticipateMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAnticipateMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ANTICIPATE_ID.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.ANTICIPATE_ID == this.ANTICIPATE_ID.Value);
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisAnticipateMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.ANTICIPATE_IDs != null)
                {
                    listVHisAnticipateMatyExpression.Add(o => this.ANTICIPATE_IDs.Contains(o.ANTICIPATE_ID));
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listVHisAnticipateMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAnticipateMatyExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PACKING_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SUPPLIER_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SUPPLIER_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisAnticipateMatyExpression.AddRange(listVHisAnticipateMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAnticipateMatyExpression.Clear();
                search.listVHisAnticipateMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
