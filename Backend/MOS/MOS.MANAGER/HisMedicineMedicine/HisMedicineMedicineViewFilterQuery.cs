using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    public class HisMedicineMedicineViewFilterQuery : HisMedicineMedicineViewFilter
    {
        public HisMedicineMedicineViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MEDICINE, bool>>> listVHisMedicineMedicineExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDICINE_MEDICINE, bool>>>();



        internal HisMedicineMedicineSO Query()
        {
            HisMedicineMedicineSO search = new HisMedicineMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMedicineMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMedicineMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMedicineMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_ID.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.MEDICINE_ID == this.MEDICINE_ID.Value);
                }
                if (this.MEDICINE_IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => this.MEDICINE_IDs.Contains(o.MEDICINE_ID));
                }
                if (this.PREPARATION_MEDICINE_ID.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.PREPARATION_MEDICINE_ID == this.PREPARATION_MEDICINE_ID.Value);
                }
                if (this.PREPARATION_MEDICINE_IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => this.PREPARATION_MEDICINE_IDs.Contains(o.PREPARATION_MEDICINE_ID));
                }
                if (this.PREPARATION_MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.PREPARATION_MEDICINE_TYPE_ID == this.PREPARATION_MEDICINE_TYPE_ID.Value);
                }
                if (this.PREPARATION_MEDICINE_TYPE_IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => this.PREPARATION_MEDICINE_TYPE_IDs.Contains(o.PREPARATION_MEDICINE_TYPE_ID));
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.SUPPLIER_ID.HasValue && o.SUPPLIER_ID.Value == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listVHisMedicineMedicineExpression.Add(o => o.SUPPLIER_ID.HasValue && this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID.Value));
                }

                search.listVHisMedicineMedicineExpression.AddRange(listVHisMedicineMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMedicineMedicineExpression.Clear();
                search.listVHisMedicineMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
