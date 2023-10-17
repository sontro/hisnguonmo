using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServRation
{
    public class HisSereServRationFilterQuery : HisSereServRationFilter
    {
        public HisSereServRationFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_RATION, bool>>> listHisSereServRationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_RATION, bool>>>();



        internal HisSereServRationSO Query()
        {
            HisSereServRationSO search = new HisSereServRationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisSereServRationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSereServRationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSereServRationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSereServRationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisSereServRationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_ID.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisSereServRationExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SERVICE_REQ_ID.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.SERVICE_REQ_ID == this.SERVICE_REQ_ID.Value);
                }
                if (this.SERVICE_REQ_IDs != null)
                {
                    listHisSereServRationExpression.Add(o => this.SERVICE_REQ_IDs.Contains(o.SERVICE_REQ_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listHisSereServRationExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listHisSereServRationExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }

                search.listHisSereServRationExpression.AddRange(listHisSereServRationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSereServRationExpression.Clear();
                search.listHisSereServRationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
