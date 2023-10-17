using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentBedRoom
{
    public class HisTreatmentBedRoomLViewFilterQuery : HisTreatmentBedRoomLViewFilter
    {
        public HisTreatmentBedRoomLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_BED_ROOM, bool>>> listLHisTreatmentBedRoomExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_TREATMENT_BED_ROOM, bool>>>();



        internal HisTreatmentBedRoomSO Query()
        {
            HisTreatmentBedRoomSO search = new HisTreatmentBedRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.BED_ROOM_ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.BED_ROOM_ID == this.BED_ROOM_ID.Value);
                }
                if (this.BED_ROOM_IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => this.BED_ROOM_IDs.Contains(o.BED_ROOM_ID));
                }
                if (this.ADD_TIME_TO.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.ADD_TIME <= this.ADD_TIME_TO.Value);
                }
                if (this.ADD_TIME_FROM.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.ADD_TIME >= this.ADD_TIME_FROM.Value);
                }
                if (this.REMOVE_TIME_FROM.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => !o.REMOVE_TIME.HasValue || o.REMOVE_TIME.Value >= this.REMOVE_TIME_FROM.Value);
                }
                if (this.IS_IN_ROOM.HasValue && this.IS_IN_ROOM.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => !o.REMOVE_TIME.HasValue && (!o.IS_PAUSE.HasValue || o.IS_PAUSE != Constant.IS_TRUE));
                }
                if (this.IS_IN_ROOM.HasValue && !this.IS_IN_ROOM.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.REMOVE_TIME.HasValue || o.IS_PAUSE == Constant.IS_TRUE);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listLHisTreatmentBedRoomExpression.Add(o =>
                        o.BED_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_LAST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD));
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE))
                {
                    this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE = this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE.Trim().ToLower();
                    listLHisTreatmentBedRoomExpression.Add(o =>
                        o.BED_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TDL_PATIENT_UNSIGNED_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        );
                }

                if (!String.IsNullOrEmpty(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME))
                {
                    this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME = this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME.Trim().ToLower();
                    listLHisTreatmentBedRoomExpression.Add(o =>
                        o.BED_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                       );
                }
                if (this.IS_PAUSE.HasValue && this.IS_PAUSE.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.IS_PAUSE.HasValue && o.IS_PAUSE == Constant.IS_TRUE);
                }
                if (this.IS_PAUSE.HasValue && !this.IS_PAUSE.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => !o.IS_PAUSE.HasValue || o.IS_PAUSE != Constant.IS_TRUE);
                }
                if (this.HAS_CO_TREATMENT_ID.HasValue)
                {
                    if (this.HAS_CO_TREATMENT_ID.Value)
                    {
                        listLHisTreatmentBedRoomExpression.Add(o => o.CO_TREATMENT_ID.HasValue);
                    }
                    else
                    {
                        listLHisTreatmentBedRoomExpression.Add(o => !o.CO_TREATMENT_ID.HasValue);
                    }
                }
                if (this.IS_APPROVE_FINISH.HasValue)
                {
                    if (this.IS_APPROVE_FINISH.Value)
                    {
                        listLHisTreatmentBedRoomExpression.Add(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listLHisTreatmentBedRoomExpression.Add(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != Constant.IS_TRUE);
                    }
                }
                if (this.HAS_OUT_TIME.HasValue && this.HAS_OUT_TIME.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.OUT_TIME.HasValue);
                }
                if (this.HAS_OUT_TIME.HasValue && !this.HAS_OUT_TIME.Value)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => !o.OUT_TIME.HasValue);
                }
                if (this.TREATMENT_ROOM_ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TREATMENT_ROOM_ID.HasValue && o.TREATMENT_ROOM_ID == this.TREATMENT_ROOM_ID.Value);
                }
                if (this.TREATMENT_ROOM_IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TREATMENT_ROOM_ID.HasValue && this.TREATMENT_ROOM_IDs.Contains(o.TREATMENT_ROOM_ID.Value));
                }
                if (this.PATIENT_CLASSIFY_ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID == this.PATIENT_CLASSIFY_ID.Value);
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && o.TDL_TREATMENT_TYPE_ID.Value == this.TREATMENT_TYPE_ID.Value);
                }
                if (this.TREATMENT_TYPE_IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TDL_TREATMENT_TYPE_ID.HasValue && this.TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID.Value));
                }
                if (this.PATIENT_CLASSIFY_IDs != null)
                {
                    listLHisTreatmentBedRoomExpression.Add(o => o.TDL_PATIENT_CLASSIFY_ID.HasValue && this.PATIENT_CLASSIFY_IDs.Contains(o.TDL_PATIENT_CLASSIFY_ID.Value));
                }
                if (this.OBSERVED_TIME_BETWEEN.HasValue)
                {
                    listLHisTreatmentBedRoomExpression.Add(
                        o => o.TDL_OBSERVED_TIME_FROM.HasValue && o.TDL_OBSERVED_TIME_FROM.Value <= this.OBSERVED_TIME_BETWEEN
                            && o.TDL_OBSERVED_TIME_TO.HasValue && this.OBSERVED_TIME_BETWEEN <= o.TDL_OBSERVED_TIME_TO.Value
                            );
                }

                search.listLHisTreatmentBedRoomExpression.AddRange(listLHisTreatmentBedRoomExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisTreatmentBedRoomExpression.Clear();
                search.listLHisTreatmentBedRoomExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
