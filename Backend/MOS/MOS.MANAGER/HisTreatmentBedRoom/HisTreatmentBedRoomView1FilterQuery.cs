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
    public class HisTreatmentBedRoomView1FilterQuery : HisTreatmentBedRoomView1Filter
    {
        public HisTreatmentBedRoomView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BED_ROOM_1, bool>>> listVHisTreatmentBedRoom1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_BED_ROOM_1, bool>>>();



        internal HisTreatmentBedRoomSO Query()
        {
            HisTreatmentBedRoomSO search = new HisTreatmentBedRoomSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.BED_ROOM_ID.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.BED_ROOM_ID == this.BED_ROOM_ID.Value);
                }
                if (this.ADD_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.ADD_TIME >= this.ADD_TIME_FROM.Value);
                }
                if (this.ADD_TIME_TO.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.ADD_TIME <= this.ADD_TIME_TO.Value);
                }
                if (this.REMOVE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => !o.REMOVE_TIME.HasValue || o.REMOVE_TIME.Value >= this.REMOVE_TIME_FROM.Value);
                }
                if (this.IS_IN_ROOM.HasValue && this.IS_IN_ROOM.Value)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => !o.REMOVE_TIME.HasValue);
                }
                if (this.IS_IN_ROOM.HasValue && !this.IS_IN_ROOM.Value)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.REMOVE_TIME.HasValue);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.BED_ID.HasValue)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.BED_ID.HasValue && o.BED_ID.Value == this.BED_ID.Value);
                }
                if (this.BED_IDs != null)
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.BED_ID.HasValue && this.BED_IDs.Contains(o.BED_ID.Value));
                }

                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisTreatmentBedRoom1Expression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (this.HAS_CO_TREATMENT_ID.HasValue)
                {
                    if (this.HAS_CO_TREATMENT_ID.Value)
                    {
                        listVHisTreatmentBedRoom1Expression.Add(o => o.CO_TREATMENT_ID.HasValue);
                    }
                    else
                    {
                        listVHisTreatmentBedRoom1Expression.Add(o => !o.CO_TREATMENT_ID.HasValue);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatmentBedRoom1Expression.Add(o =>
                        o.ADD_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ADD_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BED_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_LAST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REMOVE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REMOVE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD));
                }
                if (!String.IsNullOrEmpty(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE))
                {
                    this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE = this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE.Trim().ToLower();
                    listVHisTreatmentBedRoom1Expression.Add(o =>
                        o.BED_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE));
                }

                if (!String.IsNullOrEmpty(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME))
                {
                    this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME = this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME.Trim().ToLower();
                    listVHisTreatmentBedRoom1Expression.Add(o =>
                        o.BED_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                        || o.TREATMENT_CODE.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME)
                       );
                }

                if (this.IS_APPROVE_FINISH.HasValue)
                {
                    if (this.IS_APPROVE_FINISH.Value)
                    {
                        listVHisTreatmentBedRoom1Expression.Add(o => o.IS_APPROVE_FINISH.HasValue && o.IS_APPROVE_FINISH.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisTreatmentBedRoom1Expression.Add(o => !o.IS_APPROVE_FINISH.HasValue || o.IS_APPROVE_FINISH.Value != Constant.IS_TRUE);
                    }
                }

                search.listVHisTreatmentBedRoom1Expression.AddRange(listVHisTreatmentBedRoom1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentBedRoom1Expression.Clear();
                search.listVHisTreatmentBedRoom1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
