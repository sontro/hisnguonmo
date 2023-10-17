using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMedicine
{
    public class HisExpMestMedicineView5FilterQuery : HisExpMestMedicineView5Filter
    {
        public HisExpMestMedicineView5FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MEDICINE_5, bool>>> listVHisExpMestMedicine5Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXP_MEST_MEDICINE_5, bool>>>();

        internal HisExpMestMedicineSO Query()
        {
            HisExpMestMedicineSO search = new HisExpMestMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.EXP_MEST_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.EXP_MEST_ID == this.EXP_MEST_ID.Value);
                }
                if (this.EXP_MEST_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.EXP_MEST_ID.HasValue && this.EXP_MEST_IDs.Contains(o.EXP_MEST_ID.Value));
                }
                if (this.MEDICINE_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.MEDICINE_ID.HasValue && this.MEDICINE_IDs.Contains(o.MEDICINE_ID.Value));
                }
                if (this.MEDICINE_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => this.MEDICINE_ID.Value == o.MEDICINE_ID);
                }
                if (this.TDL_MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_MEDICINE_TYPE_ID == this.TDL_MEDICINE_TYPE_ID.Value);
                }
                if (this.TDL_MEDICINE_TYPE_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_MEDICINE_TYPE_ID.HasValue && this.TDL_MEDICINE_TYPE_IDs.Contains(o.TDL_MEDICINE_TYPE_ID.Value));
                }
                if (this.EXP_TIME_FROM.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.EXP_TIME.Value >= this.EXP_TIME_FROM.Value);
                }
                if (this.EXP_TIME_TO.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.EXP_TIME.Value <= this.EXP_TIME_TO.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.PATIENT_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => this.PATIENT_IDs.Contains(o.PATIENT_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.PATIENT_TYPE_ID.HasValue && o.PATIENT_TYPE_ID.Value == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.PATIENT_TYPE_ID.HasValue && this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID.Value));
                }
                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && o.TDL_SERVICE_REQ_ID.Value == this.TDL_SERVICE_REQ_ID.Value);
                }
                if (this.TDL_SERVICE_REQ_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && this.TDL_SERVICE_REQ_IDs.Contains(o.TDL_SERVICE_REQ_ID.Value));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && o.TDL_TREATMENT_ID.Value == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.TDL_VACCINATION_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_VACCINATION_ID.HasValue && o.TDL_VACCINATION_ID == this.TDL_VACCINATION_ID.Value);
                }
                if (this.TDL_VACCINATION_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.TDL_VACCINATION_ID.HasValue && this.TDL_VACCINATION_IDs.Contains(o.TDL_VACCINATION_ID.Value));
                }
                if (this.VACCINATION_RESULT_ID.HasValue)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.VACCINATION_RESULT_ID.HasValue && o.VACCINATION_RESULT_ID == this.VACCINATION_RESULT_ID.Value);
                }
                if (this.VACCINATION_RESULT_IDs != null)
                {
                    listVHisExpMestMedicine5Expression.Add(o => o.VACCINATION_RESULT_ID.HasValue && this.VACCINATION_RESULT_IDs.Contains(o.VACCINATION_RESULT_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower();
                    listVHisExpMestMedicine5Expression.Add(o => o.APP_CREATOR.Contains(this.KEY_WORD)
                        || o.ANTIGEN_NAMES.Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.Contains(this.KEY_WORD)
                        || o.APPROVAL_LOGINNAME.Contains(this.KEY_WORD)
                        || o.APPROVAL_USERNAME.Contains(this.KEY_WORD)
                        || o.CREATOR.Contains(this.KEY_WORD)
                        || o.DESCRIPTION.Contains(this.KEY_WORD)
                        || o.EXECUTE_LOGINNAME.Contains(this.KEY_WORD)
                        || o.EXECUTE_USERNAME.Contains(this.KEY_WORD)
                        || o.EXP_LOGINNAME.Contains(this.KEY_WORD)
                        || o.EXP_USERNAME.Contains(this.KEY_WORD)
                        || o.GROUP_CODE.Contains(this.KEY_WORD)
                        || o.MEDICINE_TYPE_CODE.Contains(this.KEY_WORD)
                        || o.MEDICINE_TYPE_NAME.Contains(this.KEY_WORD)
                        || o.MODIFIER.Contains(this.KEY_WORD)
                        || o.REQUEST_LOGINNAME.Contains(this.KEY_WORD)
                        || o.REQUEST_USERNAME.Contains(this.KEY_WORD)
                        || o.TUTORIAL.Contains(this.KEY_WORD)
                        || o.VACCINATION_RESULT_CODE.Contains(this.KEY_WORD)
                        || o.VACCINATION_RESULT_NAME.Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisExpMestMedicine5Expression.AddRange(listVHisExpMestMedicine5Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMedicine5Expression.Clear();
                search.listVHisExpMestMedicine5Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
