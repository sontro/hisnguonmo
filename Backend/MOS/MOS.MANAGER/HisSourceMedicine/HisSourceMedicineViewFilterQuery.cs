using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    public class HisSourceMedicineViewFilterQuery : HisSourceMedicineViewFilter
    {
        public HisSourceMedicineViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SOURCE_MEDICINE, bool>>> listVHisSourceMedicineExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SOURCE_MEDICINE, bool>>>();

        

        internal HisSourceMedicineSO Query()
        {
            HisSourceMedicineSO search = new HisSourceMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisSourceMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisSourceMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisSourceMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisSourceMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisSourceMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisSourceMedicineExpression.AddRange(listVHisSourceMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSourceMedicineExpression.Clear();
                search.listVHisSourceMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
