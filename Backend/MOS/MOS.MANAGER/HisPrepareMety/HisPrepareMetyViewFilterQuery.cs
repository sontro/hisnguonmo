using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    public class HisPrepareMetyViewFilterQuery : HisPrepareMetyViewFilter
    {
        public HisPrepareMetyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_METY, bool>>> listVHisPrepareMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_METY, bool>>>();



        internal HisPrepareMetySO Query()
        {
            HisPrepareMetySO search = new HisPrepareMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPrepareMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPrepareMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPrepareMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.PREPARE_ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.PREPARE_ID == this.PREPARE_ID.Value);
                }
                if (this.PREPARE_IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.PREPARE_IDs.Contains(o.PREPARE_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }
                if (this.MANUFACTURER_ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.MANUFACTURER_ID.HasValue && o.MANUFACTURER_ID.Value == this.MANUFACTURER_ID.Value);
                }
                if (this.MANUFACTURER_IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => o.MANUFACTURER_ID.HasValue && this.MANUFACTURER_IDs.Contains(o.MANUFACTURER_ID.Value));
                }
                if (this.TDL_SERVICE_UNIT_ID.HasValue)
                {
                    listVHisPrepareMetyExpression.Add(o => o.TDL_SERVICE_UNIT_ID == this.TDL_SERVICE_UNIT_ID.Value);
                }
                if (this.TDL_SERVICE_UNIT_IDs != null)
                {
                    listVHisPrepareMetyExpression.Add(o => this.TDL_SERVICE_UNIT_IDs.Contains(o.TDL_SERVICE_UNIT_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.MANUFACTURER_CODE__EXACT))
                {
                    listVHisPrepareMetyExpression.Add(o => o.MANUFACTURER_CODE == this.MANUFACTURER_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.MEDICINE_TYPE_CODE__EXACT))
                {
                    listVHisPrepareMetyExpression.Add(o => o.MEDICINE_TYPE_CODE == this.MEDICINE_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_UNIT_CODE__EXACT))
                {
                    listVHisPrepareMetyExpression.Add(o => o.SERVICE_UNIT_CODE == this.SERVICE_UNIT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQ_LOGINNAME__EXACT))
                {
                    listVHisPrepareMetyExpression.Add(o => o.REQ_LOGINNAME == this.REQ_LOGINNAME__EXACT);
                }

                search.listVHisPrepareMetyExpression.AddRange(listVHisPrepareMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPrepareMetyExpression.Clear();
                search.listVHisPrepareMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
