using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    public class HisTreatmentRoomFilterQuery : HisTreatmentRoomFilter
    {
        public HisTreatmentRoomFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_ROOM, bool>>> listHisTreatmentRoomExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_ROOM, bool>>>();

        

        internal HisTreatmentRoomSO Query()
        {
            HisTreatmentRoomSO search = new HisTreatmentRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTreatmentRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTreatmentRoomExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTreatmentRoomExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTreatmentRoomExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BED_ROOM_ID.HasValue)
                {
                    listHisTreatmentRoomExpression.Add(o => o.BED_ROOM_ID == this.BED_ROOM_ID.Value);
                }
                if (this.BED_ROOM_IDs != null)
                {
                    listHisTreatmentRoomExpression.Add(o => this.BED_ROOM_IDs.Contains(o.BED_ROOM_ID));
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listHisTreatmentRoomExpression.Add(o =>
                        o.TREATMENT_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisTreatmentRoomExpression.AddRange(listHisTreatmentRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTreatmentRoomExpression.Clear();
                search.listHisTreatmentRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
