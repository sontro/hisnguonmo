using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    public class HisPatientTypeRoomViewFilterQuery : HisPatientTypeRoomViewFilter
    {
        public HisPatientTypeRoomViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_TYPE_ROOM, bool>>> listVHisPatientTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_TYPE_ROOM, bool>>>();

        

        internal HisPatientTypeRoomSO Query()
        {
            HisPatientTypeRoomSO search = new HisPatientTypeRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPatientTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPatientTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.ROOM_ID.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisPatientTypeRoomExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisPatientTypeRoomExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.ROOM_TYPE_ID.HasValue)
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.ROOM_TYPE_ID == this.ROOM_TYPE_ID.Value);
                }
                if (this.ROOM_TYPE_IDs != null)
                {
                    listVHisPatientTypeRoomExpression.Add(o => this.ROOM_TYPE_IDs.Contains(o.ROOM_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.ROOM_CODE__EXACT))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.ROOM_CODE == this.ROOM_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.PATIENT_TYPE_CODE__EXACT))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.PATIENT_TYPE_CODE == this.PATIENT_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.ROOM_TYPE_CODE__EXACT))
                {
                    listVHisPatientTypeRoomExpression.Add(o => o.ROOM_TYPE_CODE == this.ROOM_TYPE_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisPatientTypeRoomExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PATIENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisPatientTypeRoomExpression.AddRange(listVHisPatientTypeRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPatientTypeRoomExpression.Clear();
                search.listVHisPatientTypeRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
