using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationExam
{
    public class HisVaccinationExamViewFilterQuery : HisVaccinationExamViewFilter
    {
        public HisVaccinationExamViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_EXAM, bool>>> listVHisVaccinationExamExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_VACCINATION_EXAM, bool>>>();



        internal HisVaccinationExamSO Query()
        {
            HisVaccinationExamSO search = new HisVaccinationExamSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisVaccinationExamExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisVaccinationExamExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisVaccinationExamExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion


                if (this.BRANCH_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.EXECUTE_DEPARTMENT_IDs.Contains(o.EXECUTE_DEPARTMENT_ID));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.REQUEST_DEPARTMENT_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.REQUEST_DEPARTMENT_IDs.Contains(o.REQUEST_DEPARTMENT_ID));
                }
                if (this.REQUEST_ROOM_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_ROOM_ID == this.REQUEST_ROOM_ID.Value);
                }
                if (this.REQUEST_ROOM_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => this.REQUEST_ROOM_IDs.Contains(o.REQUEST_ROOM_ID));
                }
                if (this.TDL_PATIENT_GENDER_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.TDL_PATIENT_GENDER_ID.HasValue && o.TDL_PATIENT_GENDER_ID.Value == this.REQUEST_ROOM_ID.Value);
                }
                if (this.TDL_PATIENT_GENDER_IDs != null)
                {
                    listVHisVaccinationExamExpression.Add(o => o.TDL_PATIENT_GENDER_ID.HasValue && this.TDL_PATIENT_GENDER_IDs.Contains(o.TDL_PATIENT_GENDER_ID.Value));
                }
                if (this.EXECUTE_DATE_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_DATE.HasValue && o.EXECUTE_DATE.Value >= this.EXECUTE_DATE_FROM.Value);
                }
                if (this.EXECUTE_DATE_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_DATE.HasValue && o.EXECUTE_DATE.Value <= this.EXECUTE_DATE_TO.Value);
                }
                if (this.EXECUTE_TIME_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_TIME.HasValue && o.EXECUTE_TIME.Value >= this.EXECUTE_TIME_FROM.Value);
                }
                if (this.EXECUTE_TIME_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_TIME.HasValue && o.EXECUTE_TIME.Value <= this.EXECUTE_TIME_TO.Value);
                }
                if (this.REQUEST_DATE_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_DATE >= this.REQUEST_DATE_FROM.Value);
                }
                if (this.REQUEST_DATE_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_DATE <= this.REQUEST_DATE_TO.Value);
                }
                if (this.REQUEST_TIME_FROM.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_TIME >= this.REQUEST_TIME_FROM.Value);
                }
                if (this.REQUEST_TIME_TO.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_TIME <= this.REQUEST_TIME_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.EXECUTE_LOGINNAME__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_LOGINNAME == this.EXECUTE_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQUEST_LOGINNAME__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_LOGINNAME == this.REQUEST_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.VACCINATION_EXAM_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.VACCINATION_EXAM_CODE == this.VACCINATION_EXAM_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.EXECUTE_DEPARTMENT_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_DEPARTMENT_CODE == this.EXECUTE_DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.EXECUTE_ROOM_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.EXECUTE_ROOM_CODE == this.EXECUTE_ROOM_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.PATIENT_TYPE_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.PATIENT_TYPE_CODE == this.PATIENT_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQUEST_DEPARTMENT_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_DEPARTMENT_CODE == this.REQUEST_DEPARTMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQUEST_ROOM_CODE__EXACT))
                {
                    listVHisVaccinationExamExpression.Add(o => o.REQUEST_ROOM_CODE == this.REQUEST_ROOM_CODE__EXACT);
                }
                if (this.VACCINATION_EXAM_STT_ID.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.VACCINATION_EXAM_STT_ID == this.VACCINATION_EXAM_STT_ID);
                }

                if (this.HAS_EXECUTE_TIME.HasValue)
                {
                    if (this.HAS_EXECUTE_TIME.Value)
                    {
                        listVHisVaccinationExamExpression.Add(o => o.EXECUTE_TIME.HasValue);
                    }
                    else
                    {
                        listVHisVaccinationExamExpression.Add(o => !o.EXECUTE_TIME.HasValue);
                    }
                }
                if (this.CONCLUDE.HasValue)
                {
                    listVHisVaccinationExamExpression.Add(o => o.CONCLUDE == this.CONCLUDE.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisVaccinationExamExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.NOTE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CAREER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_LAST_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_WORK_PLACE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_WORK_PLACE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.VACCINATION_EXAM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }


                search.listVHisVaccinationExamExpression.AddRange(listVHisVaccinationExamExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisVaccinationExamExpression.Clear();
                search.listVHisVaccinationExamExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
