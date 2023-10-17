using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoldReturn
{
    public class HisHoldReturnFilterQuery : HisHoldReturnFilter
    {
        public HisHoldReturnFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HOLD_RETURN, bool>>> listHisHoldReturnExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HOLD_RETURN, bool>>>();



        internal HisHoldReturnSO Query()
        {
            HisHoldReturnSO search = new HisHoldReturnSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHoldReturnExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHoldReturnExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHoldReturnExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.PATIENT_ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                
                if (this.RESPONSIBLE_ROOM_ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.RESPONSIBLE_ROOM_ID == this.RESPONSIBLE_ROOM_ID.Value);
                }
                if (this.RESPONSIBLE_ROOM_IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => this.RESPONSIBLE_ROOM_IDs.Contains(o.RESPONSIBLE_ROOM_ID));
                }
                if (this.HOLD_ROOM_ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.HOLD_ROOM_ID == this.HOLD_ROOM_ID.Value);
                }
                if (this.HOLD_ROOM_IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => this.HOLD_ROOM_IDs.Contains(o.HOLD_ROOM_ID));
                }
                if (this.RETURN_ROOM_ID.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.RETURN_ROOM_ID.HasValue && o.RETURN_ROOM_ID.Value == this.RETURN_ROOM_ID.Value);
                }
                if (this.RETURN_ROOM_IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => o.RETURN_ROOM_ID.HasValue && this.RETURN_ROOM_IDs.Contains(o.RETURN_ROOM_ID.Value));
                }

                if (this.HOLD_TIME_FROM.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.HOLD_TIME >= this.HOLD_TIME_FROM.Value);
                }
                if (this.HOLD_TIME_TO.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.HOLD_TIME <= this.HOLD_TIME_TO.Value);
                }
                if (this.RETURN_TIME_FROM.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.RETURN_TIME.HasValue && o.RETURN_TIME.Value >= this.RETURN_TIME_FROM.Value);
                }
                if (this.RETURN_TIME_TO.HasValue)
                {
                    listHisHoldReturnExpression.Add(o => o.RETURN_TIME.HasValue && o.RETURN_TIME.Value <= this.CREATE_TIME_TO.Value);
                }

                if (this.IS_HANDOVERING.HasValue)
                {
                    if (this.IS_HANDOVERING.Value)
                    {
                        listHisHoldReturnExpression.Add(o => o.IS_HANDOVERING.HasValue && o.IS_HANDOVERING.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisHoldReturnExpression.Add(o => !o.IS_HANDOVERING.HasValue || o.IS_HANDOVERING.Value != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_RETURN_TIME.HasValue)
                {
                    if (this.HAS_RETURN_TIME.Value)
                    {
                        listHisHoldReturnExpression.Add(o => o.RETURN_TIME.HasValue);
                    }
                    else
                    {
                        listHisHoldReturnExpression.Add(o => !o.RETURN_TIME.HasValue);
                    }
                }

                if (this.IS_NOT_HANDOVERING_OR_IDs != null)
                {
                    listHisHoldReturnExpression.Add(o => (!o.IS_HANDOVERING.HasValue || o.IS_HANDOVERING.Value != Constant.IS_TRUE) || this.IS_NOT_HANDOVERING_OR_IDs.Contains(o.ID));
                }

                if (!String.IsNullOrWhiteSpace(this.HOLD_LOGINNAME__EXACT))
                {
                    listHisHoldReturnExpression.Add(o => o.HOLD_LOGINNAME == this.HOLD_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.HEIN_CARD_NUMBER__EXACT))
                {
                    listHisHoldReturnExpression.Add(o => o.HEIN_CARD_NUMBER == this.HEIN_CARD_NUMBER__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.RETURN_LOGINNAME__EXACT))
                {
                    listHisHoldReturnExpression.Add(o => o.RETURN_LOGINNAME == this.RETURN_LOGINNAME__EXACT);
                }

                search.listHisHoldReturnExpression.AddRange(listHisHoldReturnExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHoldReturnExpression.Clear();
                search.listHisHoldReturnExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
