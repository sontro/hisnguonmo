using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public class HisSereServView13FilterQuery : HisSereServView13Filter
    {
        public HisSereServView13FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_13, bool>>> listVHisSereServ13Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_13, bool>>>();



        internal HisSereServSO Query()
        {
            HisSereServSO search = new HisSereServSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSereServ13Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSereServ13Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSereServ13Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisSereServ13Expression.Add(o => o.HEIN_CARD_NUMBER != null && o.HEIN_CARD_NUMBER.Equals(this.HEIN_CARD_NUMBER__EXACT));
                }
                if (!String.IsNullOrEmpty(this.HEIN_CARD_NUMBER))
                {
                    listVHisSereServ13Expression.Add(o => o.HEIN_CARD_NUMBER != null && o.HEIN_CARD_NUMBER.ToLower().Contains(this.HEIN_CARD_NUMBER.ToLower().Trim()));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.IS_SPECIMEN.HasValue && this.IS_SPECIMEN.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.IS_SPECIMEN.HasValue && o.IS_SPECIMEN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_SPECIMEN.HasValue && !this.IS_SPECIMEN.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.IS_SPECIMEN.HasValue || o.IS_SPECIMEN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.IS_EXPEND.HasValue && this.IS_EXPEND.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.IS_EXPEND.HasValue && o.IS_EXPEND.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPEND.HasValue && !this.IS_EXPEND.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.IS_EXPEND.HasValue || o.IS_EXPEND.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.INVOICE_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.INVOICE_ID.HasValue && o.INVOICE_ID.Value == this.INVOICE_ID.Value);
                }
                if (this.HAS_INVOICE.HasValue && !this.HAS_INVOICE.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.INVOICE_ID.HasValue);
                }
                if (this.HAS_INVOICE.HasValue && this.HAS_INVOICE.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.INVOICE_ID.HasValue);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HEIN_APPROVAL_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.HEIN_APPROVAL_ID.HasValue && o.HEIN_APPROVAL_ID.Value == this.HEIN_APPROVAL_ID.Value);
                }
                if (this.HEIN_APPROVAL_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => o.HEIN_APPROVAL_ID.HasValue && this.HEIN_APPROVAL_IDs.Contains(o.HEIN_APPROVAL_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.TDL_TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_MEDICINE.HasValue && this.IS_MEDICINE.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.MEDICINE_ID.HasValue);
                }
                if (this.IS_MEDICINE.HasValue && !this.IS_MEDICINE.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.MEDICINE_ID.HasValue);
                }
                if (this.IS_MATERIAL.HasValue && this.IS_MATERIAL.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.MATERIAL_ID.HasValue);
                }
                if (this.IS_MATERIAL.HasValue && !this.IS_MATERIAL.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.MATERIAL_ID.HasValue);
                }
                if (this.IS_BLOOD.HasValue && this.IS_BLOOD.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.BLOOD_ID.HasValue);
                }
                if (this.IS_BLOOD.HasValue && !this.IS_BLOOD.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.BLOOD_ID.HasValue);
                }
                if (this.PTTT_APPROVAL_STT_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.PTTT_APPROVAL_STT_ID == this.PTTT_APPROVAL_STT_ID.Value);
                }
                if (this.PTTT_APPROVAL_STT_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => o.PTTT_APPROVAL_STT_ID.HasValue && this.PTTT_APPROVAL_STT_IDs.Contains(o.PTTT_APPROVAL_STT_ID.Value));
                }
                if (this.PTTT_CALENDAR_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.PTTT_CALENDAR_ID == this.PTTT_CALENDAR_ID.Value);
                }
                if (this.PTTT_CALENDAR_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => o.PTTT_CALENDAR_ID.HasValue && this.PTTT_CALENDAR_IDs.Contains(o.PTTT_CALENDAR_ID.Value));
                }
                if (this.EXECUTE_DEPARTMENT_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.EXECUTE_DEPARTMENT_ID == this.EXECUTE_DEPARTMENT_ID.Value);
                }
                if (this.EXECUTE_DEPARTMENT_IDs != null)
                {
                    listVHisSereServ13Expression.Add(o => this.EXECUTE_DEPARTMENT_IDs.Contains(o.EXECUTE_DEPARTMENT_ID));
                }
                if (this.IS_IN_CALENDAR.HasValue && this.IS_IN_CALENDAR.Value)
                {
                    listVHisSereServ13Expression.Add(o => o.PTTT_CALENDAR_ID.HasValue);
                }
                if (this.IS_IN_CALENDAR.HasValue && !this.IS_IN_CALENDAR.Value)
                {
                    listVHisSereServ13Expression.Add(o => !o.PTTT_CALENDAR_ID.HasValue);
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.TDL_SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listVHisSereServ13Expression.Add(o => o.TDL_REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }

                if (!String.IsNullOrWhiteSpace(KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisSereServ13Expression.Add(o => o.TDL_SERVICE_REQ_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_LAST_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TDL_PATIENT_FIRST_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisSereServ13Expression.AddRange(listVHisSereServ13Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServ13Expression.Clear();
                search.listVHisSereServ13Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
