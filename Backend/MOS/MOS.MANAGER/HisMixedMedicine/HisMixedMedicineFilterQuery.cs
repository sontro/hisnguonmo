using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    public class HisMixedMedicineFilterQuery : HisMixedMedicineFilter
    {
        public HisMixedMedicineFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MIXED_MEDICINE, bool>>> listHisMixedMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MIXED_MEDICINE, bool>>>();

        

        internal HisMixedMedicineSO Query()
        {
            HisMixedMedicineSO search = new HisMixedMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMixedMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMixedMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMixedMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMixedMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.INFUSION_ID.HasValue)
                {
                    listHisMixedMedicineExpression.Add(o => o.INFUSION_ID == this.INFUSION_ID.Value);
                }
                if (this.INFUSION_IDs != null)
                {
                    listHisMixedMedicineExpression.Add(o => this.INFUSION_IDs.Contains(o.ID));
                }

                search.listHisMixedMedicineExpression.AddRange(listHisMixedMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMixedMedicineExpression.Clear();
                search.listHisMixedMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
