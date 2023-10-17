using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMety
{
    public class HisAnticipateMetyViewFilterQuery : HisAnticipateMetyViewFilter
    {
        public HisAnticipateMetyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTICIPATE_METY, bool>>> listVHisAnticipateMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTICIPATE_METY, bool>>>();

        

        internal HisAnticipateMetySO Query()
        {
            HisAnticipateMetySO search = new HisAnticipateMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAnticipateMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAnticipateMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAnticipateMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAnticipateMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ANTICIPATE_ID.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.ANTICIPATE_ID == this.ANTICIPATE_ID.Value);
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisAnticipateMetyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.ANTICIPATE_IDs != null)
                {
                    listVHisAnticipateMetyExpression.Add(o => this.ANTICIPATE_IDs.Contains(o.ANTICIPATE_ID));
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisAnticipateMetyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAnticipateMetyExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BYT_NUM_ORDER.ToLower().Contains(this.KEY_WORD) ||
                        o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_TYPE_PROPRIETARY_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_USE_FORM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICINE_USE_FORM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PACKING_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REGISTER_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SUPPLIER_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SUPPLIER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TCY_NUM_ORDER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisAnticipateMetyExpression.AddRange(listVHisAnticipateMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAnticipateMetyExpression.Clear();
                search.listVHisAnticipateMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
