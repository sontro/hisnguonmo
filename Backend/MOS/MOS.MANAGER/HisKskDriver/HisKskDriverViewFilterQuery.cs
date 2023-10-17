using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    public class HisKskDriverViewFilterQuery : HisKskDriverViewFilter
    {
        public HisKskDriverViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_DRIVER, bool>>> listVHisKskDriverExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_DRIVER, bool>>>();



        internal HisKskDriverSO Query()
        {
            HisKskDriverSO search = new HisKskDriverSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisKskDriverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisKskDriverExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisKskDriverExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisKskDriverExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisKskDriverExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listVHisKskDriverExpression.Add(o => this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisKskDriverExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }
                if (this.EXECUTE_ROOM_ID.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.EXECUTE_ROOM_ID == this.EXECUTE_ROOM_ID.Value);
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisKskDriverExpression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.EXECUTE_ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KSK_DRIVER_CODE__EXACT))
                {
                    listVHisKskDriverExpression.Add(o => o.KSK_DRIVER_CODE == this.KSK_DRIVER_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisKskDriverExpression.Add(o => o.TDL_TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.PATIENT_CODE__EXACT))
                {
                    listVHisKskDriverExpression.Add(o => o.TDL_PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_REQ_CODE__EXACT))
                {
                    listVHisKskDriverExpression.Add(o => o.SERVICE_REQ_CODE == this.SERVICE_REQ_CODE__EXACT);
                }

                if (this.CONCLUSION_DATE__EQUAL.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value == this.CONCLUSION_DATE__EQUAL);
                }
                if (this.CONCLUSION_DATE_FROM.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value >= this.CONCLUSION_DATE_FROM);
                }
                if (this.CONCLUSION_DATE_TO.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value <= this.CONCLUSION_DATE_TO);
                }

                if (this.SYNC_RESULT_TYPE.HasValue)
                {
                    listVHisKskDriverExpression.Add(o => o.SYNC_RESULT_TYPE == this.SYNC_RESULT_TYPE.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisKskDriverExpression.Add(o => o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_GENDER_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CONCLUDER_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.CONCLUSION.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.LICENSE_CLASS.ToLower().Contains(this.KEY_WORD)
                        || o.KSK_DRIVER_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REASON_BAD_HEATHLY.ToLower().Contains(this.KEY_WORD)
                        || o.SICK_CONDITION.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_REQ_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_TREATMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisKskDriverExpression.AddRange(listVHisKskDriverExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisKskDriverExpression.Clear();
                search.listVHisKskDriverExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
