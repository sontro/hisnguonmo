using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriver
{
    public class HisKskDriverFilterQuery : HisKskDriverFilter
    {
        public HisKskDriverFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_DRIVER, bool>>> listHisKskDriverExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_DRIVER, bool>>>();



        internal HisKskDriverSO Query()
        {
            HisKskDriverSO search = new HisKskDriverSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisKskDriverExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskDriverExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskDriverExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskDriverExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listHisKskDriverExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.TDL_PATIENT_ID.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.TDL_PATIENT_ID == this.TDL_PATIENT_ID.Value);
                }
                if (this.TDL_PATIENT_IDs != null)
                {
                    listHisKskDriverExpression.Add(o => this.TDL_PATIENT_IDs.Contains(o.TDL_PATIENT_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listHisKskDriverExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KSK_DRIVER_CODE__EXACT))
                {
                    listHisKskDriverExpression.Add(o => o.KSK_DRIVER_CODE == this.KSK_DRIVER_CODE__EXACT);
                }

                if (this.CONCLUSION_DATE__EQUAL.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value == this.CONCLUSION_DATE__EQUAL);
                }
                if (this.CONCLUSION_DATE_FROM.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value >= this.CONCLUSION_DATE_FROM);
                }
                if (this.CONCLUSION_DATE_TO.HasValue)
                {
                    listHisKskDriverExpression.Add(o => o.VIR_CONCLUSION_DATE.HasValue && o.VIR_CONCLUSION_DATE.Value <= this.CONCLUSION_DATE_TO);
                }
                if (!String.IsNullOrWhiteSpace(this.LICENSE_CLASS))
                {
                    listHisKskDriverExpression.Add(o => o.LICENSE_CLASS == this.LICENSE_CLASS);
                }
                search.listHisKskDriverExpression.AddRange(listHisKskDriverExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskDriverExpression.Clear();
                search.listHisKskDriverExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
