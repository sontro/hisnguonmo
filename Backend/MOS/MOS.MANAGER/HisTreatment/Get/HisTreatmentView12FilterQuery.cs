using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Get
{
    public class HisTreatmentView12FilterQuery : HisTreatmentView12Filter
    {
        public HisTreatmentView12FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_12, bool>>> listVHisTreatment12Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_12, bool>>>();



        internal HisTreatmentSO Query()
        {
            HisTreatmentSO search = new HisTreatmentSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisTreatment12Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatment12Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatment12Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatment12Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    //Chi tim theo 1 so truong thuong su dung, de tranh van de ve hieu nang
                    this.KEY_WORD = this.KEY_WORD.Trim().ToLower();
                    listVHisTreatment12Expression.Add(o =>
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.END_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_HEIN_CARD_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.TRANSFER_IN_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.OUT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_ADDRESS.Contains(this.KEY_WORD) ||
                        o.END_DEPARTMENT_NAME.Contains(this.KEY_WORD) ||
                        o.TDL_PATIENT_GENDER_NAME.Contains(this.KEY_WORD)
                        );
                }

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisTreatment12Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }

                search.listVHisTreatment12Expression.AddRange(listVHisTreatment12Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatment12Expression.Clear();
                search.listVHisTreatment12Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
