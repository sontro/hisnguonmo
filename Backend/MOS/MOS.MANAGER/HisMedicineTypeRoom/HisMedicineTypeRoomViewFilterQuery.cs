using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    public class HisMedicineTypeRoomViewFilterQuery : HisMedicineTypeRoomViewFilter
    {
        public HisMedicineTypeRoomViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_ROOM, bool>>> listVHisMedicineTypeRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_TYPE_ROOM, bool>>>();

        

        internal HisMedicineTypeRoomSO Query()
        {
            HisMedicineTypeRoomSO search = new HisMedicineTypeRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisMedicineTypeRoomExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                search.listVHisMedicineTypeRoomExpression.AddRange(listVHisMedicineTypeRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMedicineTypeRoomExpression.Clear();
                search.listVHisMedicineTypeRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
