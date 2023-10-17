using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticRequest
{
    public class HisAntibioticRequestViewFilterQuery : HisAntibioticRequestViewFilter
    {
        public HisAntibioticRequestViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_REQUEST, bool>>> listVHisAntibioticRequestExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_REQUEST, bool>>>();

        

        internal HisAntibioticRequestSO Query()
        {
            HisAntibioticRequestSO search = new HisAntibioticRequestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAntibioticRequestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!String.IsNullOrWhiteSpace(this.ANTIBIOTIC_REQUEST_CODE__EXACT))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.ANTIBIOTIC_REQUEST_CODE == this.ANTIBIOTIC_REQUEST_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_PATIENT_CODE__EXACT))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.TDL_PATIENT_CODE == this.TDL_PATIENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.TREATMENT_CODE__EXACT))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.TREATMENT_CODE == this.TREATMENT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQUEST_LOGINNAME__EXACT))
                {
                    listVHisAntibioticRequestExpression.Add(o => o.REQUEST_LOGINNAME == this.REQUEST_LOGINNAME__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.APPROVAL_LOGINNAME__NULL_OR_EXACT))
                {
                    listVHisAntibioticRequestExpression.Add(o => String.IsNullOrEmpty(o.APPROVAL_LOGINNAME) || o.APPROVAL_LOGINNAME == this.APPROVAL_LOGINNAME__NULL_OR_EXACT);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.ANTIBIOTIC_REQUEST_STT.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.ANTIBIOTIC_REQUEST_STT == this.ANTIBIOTIC_REQUEST_STT.Value);
                }
                if (this.REQUEST_TIME__FROM.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.REQUEST_TIME >= this.REQUEST_TIME__FROM.Value);
                }
                if (this.REQUEST_TIME__TO.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.REQUEST_TIME <= this.REQUEST_TIME__TO.Value);
                }
                if (this.APPROVE_TIME__FROM.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.APPROVE_TIME.HasValue && o.APPROVE_TIME.Value >= this.APPROVE_TIME__FROM.Value);
                }
                if (this.APPROVE_TIME__TO.HasValue)
                {
                    listVHisAntibioticRequestExpression.Add(o => o.APPROVE_TIME.HasValue && o.APPROVE_TIME.Value <= this.APPROVE_TIME__TO.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisAntibioticRequestExpression.Add(o => 
                        o.ANTIBIOTIC_REQUEST_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQUEST_DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisAntibioticRequestExpression.AddRange(listVHisAntibioticRequestExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAntibioticRequestExpression.Clear();
                search.listVHisAntibioticRequestExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
