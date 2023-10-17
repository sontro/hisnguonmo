using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    public class HisTreatmentRoomViewFilterQuery : HisTreatmentRoomViewFilter
    {
        public HisTreatmentRoomViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_ROOM, bool>>> listVHisTreatmentRoomExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_ROOM, bool>>>();

        

        internal HisTreatmentRoomSO Query()
        {
            HisTreatmentRoomSO search = new HisTreatmentRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisTreatmentRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BED_ROOM_ID.HasValue)
                {
                    listVHisTreatmentRoomExpression.Add(o => o.BED_ROOM_ID == this.BED_ROOM_ID.Value);
                }
                if (this.BED_ROOM_IDs != null)
                {
                    listVHisTreatmentRoomExpression.Add(o => this.BED_ROOM_IDs.Contains(o.BED_ROOM_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentRoomExpression.Add(o =>
                        o.TREATMENT_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisTreatmentRoomExpression.AddRange(listVHisTreatmentRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentRoomExpression.Clear();
                search.listVHisTreatmentRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
