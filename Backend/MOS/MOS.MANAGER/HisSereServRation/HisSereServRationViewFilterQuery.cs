using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    public class HisSereServRationViewFilterQuery : HisSereServRationViewFilter
    {
        public HisSereServRationViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_RATION, bool>>> listVHisSereServRationExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_RATION, bool>>>();



        internal HisSereServRationSO Query()
        {
            HisSereServRationSO search = new HisSereServRationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSereServRationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSereServRationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSereServRationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.SERVICE_UNIT_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.SERVICE_UNIT_ID == this.SERVICE_UNIT_ID.Value);
                }
                if (this.SERVICE_UNIT_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.SERVICE_UNIT_IDs.Contains(o.SERVICE_UNIT_ID));
                }
                if (this.RATION_GROUP_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.RATION_GROUP_ID.HasValue && o.RATION_GROUP_ID.Value == this.RATION_GROUP_ID.Value);
                }
                if (this.RATION_GROUP_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => o.RATION_GROUP_ID.HasValue && this.RATION_GROUP_IDs.Contains(o.RATION_GROUP_ID.Value));
                }
                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (this.TRACKING_ID.HasValue)
                {
                    listVHisSereServRationExpression.Add(o => o.TRACKING_ID.HasValue && o.TRACKING_ID == this.TRACKING_ID.Value);
                }
                if (this.TRACKING_IDs != null)
                {
                    listVHisSereServRationExpression.Add(o => o.TRACKING_ID.HasValue && this.TRACKING_IDs.Contains(o.TRACKING_ID.Value));
                }
                search.listVHisSereServRationExpression.AddRange(listVHisSereServRationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServRationExpression.Clear();
                search.listVHisSereServRationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
