﻿using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    public class HisSereServView17FilterQuery: HisSereServView17Filter
    {
        public HisSereServView17FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_17, bool>>> listVHisSereServ17Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_17, bool>>>();



        internal HisSereServSO Query()
        {
            HisSereServSO search = new HisSereServSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSereServ17Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSereServ17Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSereServ17Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.HEIN_CARD_NUMBER__EXACT))
                {
                    listVHisSereServ17Expression.Add(o => o.HEIN_CARD_NUMBER != null && o.HEIN_CARD_NUMBER.Equals(this.HEIN_CARD_NUMBER__EXACT));
                }
                if (!String.IsNullOrEmpty(this.HEIN_CARD_NUMBER))
                {
                    listVHisSereServ17Expression.Add(o => o.HEIN_CARD_NUMBER != null && o.HEIN_CARD_NUMBER.ToLower().Contains(this.HEIN_CARD_NUMBER.ToLower().Trim()));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TREATMENT_ID__NOT_EQUAL.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_TREATMENT_ID != this.TREATMENT_ID__NOT_EQUAL.Value);
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => o.SERVICE_REQ_ID.HasValue && this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID.Value));
                }
                if (this.IS_SPECIMEN.HasValue && this.IS_SPECIMEN.Value)
                {
                    listVHisSereServ17Expression.Add(o => o.IS_SPECIMEN.HasValue && o.IS_SPECIMEN.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_SPECIMEN.HasValue && !this.IS_SPECIMEN.Value)
                {
                    listVHisSereServ17Expression.Add(o => !o.IS_SPECIMEN.HasValue || o.IS_SPECIMEN.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.IS_EXPEND.HasValue && this.IS_EXPEND.Value)
                {
                    listVHisSereServ17Expression.Add(o => o.IS_EXPEND.HasValue && o.IS_EXPEND.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_EXPEND.HasValue && !this.IS_EXPEND.Value)
                {
                    listVHisSereServ17Expression.Add(o => !o.IS_EXPEND.HasValue || o.IS_EXPEND.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID));
                }
                if (this.NOT_IN_SERVICE_TYPE_IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => !this.NOT_IN_SERVICE_TYPE_IDs.Contains(o.TDL_SERVICE_TYPE_ID));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => this.SERVICE_TYPE_ID == o.TDL_SERVICE_TYPE_ID);
                }
                if (this.INVOICE_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.INVOICE_ID.HasValue && o.INVOICE_ID.Value == this.INVOICE_ID.Value);
                }
                if (this.HAS_INVOICE.HasValue && !this.HAS_INVOICE.Value)
                {
                    listVHisSereServ17Expression.Add(o => !o.INVOICE_ID.HasValue);
                }
                if (this.HAS_INVOICE.HasValue && this.HAS_INVOICE.Value)
                {
                    listVHisSereServ17Expression.Add(o => o.INVOICE_ID.HasValue);
                }
                if (this.HAS_EXECUTE.HasValue && !this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ17Expression.Add(o => o.IS_NO_EXECUTE.HasValue && o.IS_NO_EXECUTE.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.HAS_EXECUTE.HasValue && this.HAS_EXECUTE.Value)
                {
                    listVHisSereServ17Expression.Add(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.NOT_IN_SERVICE_IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => !this.NOT_IN_SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.EXECUTE_ROOM_IDs != null)
                {
                    listVHisSereServ17Expression.Add(o => this.EXECUTE_ROOM_IDs.Contains(o.TDL_EXECUTE_ROOM_ID));
                }
                if (this.HAS_AMOUNT_TEMP.HasValue && !this.HAS_AMOUNT_TEMP.Value)
                {
                    listVHisSereServ17Expression.Add(o => !o.AMOUNT_TEMP.HasValue);
                }
                if (this.HAS_AMOUNT_TEMP.HasValue && this.HAS_AMOUNT_TEMP.Value)
                {
                    listVHisSereServ17Expression.Add(o => o.AMOUNT_TEMP.HasValue);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.TDL_REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.AMOUNT_TEMP.HasValue)
                {
                    listVHisSereServ17Expression.Add(o => o.AMOUNT_TEMP.Value == this.AMOUNT_TEMP.Value);
                }

                search.listVHisSereServ17Expression.AddRange(listVHisSereServ17Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServ17Expression.Clear();
                search.listVHisSereServ17Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
