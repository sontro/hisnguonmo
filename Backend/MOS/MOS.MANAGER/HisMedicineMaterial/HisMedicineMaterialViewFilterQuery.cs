using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMaterial
{
    public class HisMedicineMaterialViewFilterQuery : HisMedicineMaterialViewFilter
    {
        public HisMedicineMaterialViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MATERIAL, bool>>> listVHisMedicineMaterialExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MATERIAL, bool>>>();



        internal HisMedicineMaterialSO Query()
        {
            HisMedicineMaterialSO search = new HisMedicineMaterialSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMedicineMaterialExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMedicineMaterialExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_ID.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MEDICINE_ID == this.MEDICINE_ID.Value);
                }
                if (this.MEDICINE_IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => this.MEDICINE_IDs.Contains(o.MEDICINE_ID));
                }
                if (this.MATERIAL_ID.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MATERIAL_ID == this.MATERIAL_ID.Value);
                }
                if (this.MATERIAL_IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => this.MATERIAL_IDs.Contains(o.MATERIAL_ID));
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.SUPPLIER_ID.HasValue && o.SUPPLIER_ID.Value == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisMedicineMaterialExpression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }

                search.listVHisMedicineMaterialExpression.AddRange(listVHisMedicineMaterialExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMedicineMaterialExpression.Clear();
                search.listVHisMedicineMaterialExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
