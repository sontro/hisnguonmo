using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBaby
{
    public class HisBabyViewFilterQuery : HisBabyViewFilter
    {
        public HisBabyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BABY, bool>>> listVHisBabyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BABY, bool>>>();

        

        internal HisBabySO Query()
        {
            HisBabySO search = new HisBabySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisBabyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBabyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBabyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBabyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisBabyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BORN_POSITION_ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.BORN_POSITION_ID.HasValue && o.BORN_POSITION_ID.Value == this.BORN_POSITION_ID.Value);
                }
                if (this.BORN_POSITION_IDs != null)
                {
                    listVHisBabyExpression.Add(o => o.BORN_POSITION_ID.HasValue && this.BORN_POSITION_IDs.Contains(o.BORN_POSITION_ID.Value));
                }

                if (this.BORN_RESULT_ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.BORN_RESULT_ID.HasValue && o.BORN_RESULT_ID.Value == this.BORN_RESULT_ID.Value);
                }
                if (this.BORN_RESULT_IDs != null)
                {
                    listVHisBabyExpression.Add(o => o.BORN_RESULT_ID.HasValue && this.BORN_RESULT_IDs.Contains(o.BORN_RESULT_ID.Value));
                }

                if (this.BORN_TYPE_ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.BORN_TYPE_ID.HasValue && o.BORN_TYPE_ID.Value == this.BORN_TYPE_ID.Value);
                }
                if (this.BORN_TYPE_IDs != null)
                {
                    listVHisBabyExpression.Add(o => o.BORN_TYPE_ID.HasValue && this.BORN_TYPE_IDs.Contains(o.BORN_TYPE_ID.Value));
                }

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.TREATMENT_ID != null && o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBabyExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }

                if (this.BORN_TIME_FROM.HasValue)
                {
                    listVHisBabyExpression.Add(o =>o.BORN_TIME != null && o.BORN_TIME >= this.BORN_TIME_FROM.Value);
                }
                if (this.BORN_TIME_TO.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.BORN_TIME != null && o.BORN_TIME <= this.BORN_TIME_TO.Value);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.DEPARTMENT_ID.HasValue && o.DEPARTMENT_ID.Value == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisBabyExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.TREATMENT_CODE__EXACT))
                {
                    listVHisBabyExpression.Add(o =>o.TREATMENT_CODE != null && o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisBabyExpression.Add(o =>o.TDL_PATIENT_CODE != null && o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.PATIENT_NAME))
                {
                    this.PATIENT_NAME = this.PATIENT_NAME.Trim().ToLower();
                    listVHisBabyExpression.Add(o =>
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.PATIENT_NAME)
                        );
                }
                if (this.SYNC_RESULT_TYPE.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.SYNC_RESULT_TYPE.HasValue && o.SYNC_RESULT_TYPE.Value == this.SYNC_RESULT_TYPE.Value);
                }
                if (this.ISSUED_DATE_FROM.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.ISSUED_DATE != null && o.ISSUED_DATE >= this.ISSUED_DATE_FROM.Value);
                }
                if (this.ISSUED_DATE_TO.HasValue)
                {
                    listVHisBabyExpression.Add(o => o.ISSUED_DATE != null && o.ISSUED_DATE <= this.ISSUED_DATE_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisBabyExpression.Add(o =>
                        o.BABY_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORN_POSITION_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ETHNIC_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MIDWIFE.ToLower().Contains(this.KEY_WORD) ||
                        o.FATHER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORN_RESULT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.BORN_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GENDER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisBabyExpression.AddRange(listVHisBabyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBabyExpression.Clear();
                search.listVHisBabyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
