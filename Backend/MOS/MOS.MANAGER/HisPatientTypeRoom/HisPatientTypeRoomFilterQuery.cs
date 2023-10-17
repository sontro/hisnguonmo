using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeRoom
{
    public class HisPatientTypeRoomFilterQuery : HisPatientTypeRoomFilter
    {
        public HisPatientTypeRoomFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_TYPE_ROOM, bool>>> listHisPatientTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PATIENT_TYPE_ROOM, bool>>>();

        

        internal HisPatientTypeRoomSO Query()
        {
            HisPatientTypeRoomSO search = new HisPatientTypeRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPatientTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPatientTypeRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPatientTypeRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPatientTypeRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPatientTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisPatientTypeRoomExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listHisPatientTypeRoomExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listHisPatientTypeRoomExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }

                search.listHisPatientTypeRoomExpression.AddRange(listHisPatientTypeRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPatientTypeRoomExpression.Clear();
                search.listHisPatientTypeRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
