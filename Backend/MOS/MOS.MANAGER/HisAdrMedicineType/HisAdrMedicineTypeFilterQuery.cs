using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAdrMedicineType
{
    public class HisAdrMedicineTypeFilterQuery : HisAdrMedicineTypeFilter
    {
        public HisAdrMedicineTypeFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ADR_MEDICINE_TYPE, bool>>> listHisAdrMedicineTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ADR_MEDICINE_TYPE, bool>>>();

        

        internal HisAdrMedicineTypeSO Query()
        {
            HisAdrMedicineTypeSO search = new HisAdrMedicineTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAdrMedicineTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisAdrMedicineTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ADR_ID.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.ADR_ID == this.ADR_ID.Value);
                }
                if (this.ADR_IDs != null)
                {
                    listHisAdrMedicineTypeExpression.Add(o => this.ADR_IDs.Contains(o.ADR_ID));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listHisAdrMedicineTypeExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listHisAdrMedicineTypeExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                search.listHisAdrMedicineTypeExpression.AddRange(listHisAdrMedicineTypeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAdrMedicineTypeExpression.Clear();
                search.listHisAdrMedicineTypeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
