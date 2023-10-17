using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHoha
{
    public class HisHoreHohaViewFilterQuery : HisHoreHohaViewFilter
    {
        public HisHoreHohaViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HOHA, bool>>> listVHisHoreHohaExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_HORE_HOHA, bool>>>();

        

        internal HisHoreHohaSO Query()
        {
            HisHoreHohaSO search = new HisHoreHohaSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisHoreHohaExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisHoreHohaExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisHoreHohaExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisHoreHohaExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.HOLD_RETURN_ID.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.HOLD_RETURN_ID == this.HOLD_RETURN_ID.Value);
                }
                if (this.HOLD_RETURN_IDs != null)
                {
                    listVHisHoreHohaExpression.Add(o => this.HOLD_RETURN_IDs.Contains(o.HOLD_RETURN_ID));
                }
                if (this.HORE_HANDOVER_ID.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.HORE_HANDOVER_ID == this.HORE_HANDOVER_ID.Value);
                }
                if (this.HORE_HANDOVER_IDs != null)
                {
                    listVHisHoreHohaExpression.Add(o => this.HORE_HANDOVER_IDs.Contains(o.HORE_HANDOVER_ID));
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisHoreHohaExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }

                if (this.RETURN_TIME_FROM.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.RETURN_TIME.HasValue && o.RETURN_TIME.Value >= this.RETURN_TIME_FROM.Value);
                }
                if (this.RETURN_TIME_TO.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.RETURN_TIME.HasValue && o.RETURN_TIME.Value <= this.RETURN_TIME_TO.Value);
                }
                if (this.HOLD_TIME_FROM.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.HOLD_TIME >= this.HOLD_TIME_FROM.Value);
                }
                if (this.HOLD_TIME_TO.HasValue)
                {
                    listVHisHoreHohaExpression.Add(o => o.HOLD_TIME <= this.HOLD_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisHoreHohaExpression.Add(o => o.HEIN_CARD_NUMBER != null && o.HEIN_CARD_NUMBER == this.HEIN_CARD_NUMBER__EXACT);
                }
                if (!String.IsNullOrEmpty(this.HOLD_LOGINNAME__EXACT))
                {
                    listVHisHoreHohaExpression.Add(o => o.HOLD_LOGINNAME != null && o.HOLD_LOGINNAME == this.HOLD_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrEmpty(this.RETURN_LOGINNAME__EXACT))
                {
                    listVHisHoreHohaExpression.Add(o => o.RETURN_LOGINNAME != null && o.RETURN_LOGINNAME == this.RETURN_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisHoreHohaExpression.Add(o => o.PATIENT_CODE != null && o.PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }

                search.listVHisHoreHohaExpression.AddRange(listVHisHoreHohaExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisHoreHohaExpression.Clear();
                search.listVHisHoreHohaExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
