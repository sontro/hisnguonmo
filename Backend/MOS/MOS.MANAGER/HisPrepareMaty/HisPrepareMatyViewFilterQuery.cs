using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    public class HisPrepareMatyViewFilterQuery : HisPrepareMatyViewFilter
    {
        public HisPrepareMatyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_MATY, bool>>> listVHisPrepareMatyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PREPARE_MATY, bool>>>();



        internal HisPrepareMatySO Query()
        {
            HisPrepareMatySO search = new HisPrepareMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPrepareMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPrepareMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPrepareMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.PREPARE_ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.PREPARE_ID == this.PREPARE_ID.Value);
                }
                if (this.PREPARE_IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.PREPARE_IDs.Contains(o.PREPARE_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }
                if (this.MANUFACTURER_ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.MANUFACTURER_ID.HasValue && o.MANUFACTURER_ID.Value == this.MANUFACTURER_ID.Value);
                }
                if (this.MANUFACTURER_IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => o.MANUFACTURER_ID.HasValue && this.MANUFACTURER_IDs.Contains(o.MANUFACTURER_ID.Value));
                }
                if (this.TDL_SERVICE_UNIT_ID.HasValue)
                {
                    listVHisPrepareMatyExpression.Add(o => o.TDL_SERVICE_UNIT_ID == this.TDL_SERVICE_UNIT_ID.Value);
                }
                if (this.TDL_SERVICE_UNIT_IDs != null)
                {
                    listVHisPrepareMatyExpression.Add(o => this.TDL_SERVICE_UNIT_IDs.Contains(o.TDL_SERVICE_UNIT_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.MANUFACTURER_CODE__EXACT))
                {
                    listVHisPrepareMatyExpression.Add(o => o.MANUFACTURER_CODE == this.MANUFACTURER_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.MATERIAL_TYPE_CODE__EXACT))
                {
                    listVHisPrepareMatyExpression.Add(o => o.MATERIAL_TYPE_CODE == this.MATERIAL_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_UNIT_CODE__EXACT))
                {
                    listVHisPrepareMatyExpression.Add(o => o.SERVICE_UNIT_CODE == this.SERVICE_UNIT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.REQ_LOGINNAME__EXACT))
                {
                    listVHisPrepareMatyExpression.Add(o => o.REQ_LOGINNAME == this.REQ_LOGINNAME__EXACT);
                }

                search.listVHisPrepareMatyExpression.AddRange(listVHisPrepareMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPrepareMatyExpression.Clear();
                search.listVHisPrepareMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
