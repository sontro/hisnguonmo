using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatient
{
    public class HisPatientView1FilterQuery : HisPatientView1Filter
    {
        public HisPatientView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_1, bool>>> listVHisPatient1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PATIENT_1, bool>>>();



        internal HisPatientSO Query()
        {
            HisPatientSO search = new HisPatientSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisPatient1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisPatient1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisPatient1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisPatient1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisPatient1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    search.listVHisPatient1Expression.Add(o => o.PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }

                search.listVHisPatient1Expression.AddRange(listVHisPatient1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPatient1Expression.Clear();
                search.listVHisPatient1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
