using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    public class HisSourceMedicineFilterQuery : HisSourceMedicineFilter
    {
        public HisSourceMedicineFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SOURCE_MEDICINE, bool>>> listHisSourceMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SOURCE_MEDICINE, bool>>>();

        

        internal HisSourceMedicineSO Query()
        {
            HisSourceMedicineSO search = new HisSourceMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSourceMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSourceMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSourceMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSourceMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSourceMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisSourceMedicineExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.SOURCE_MEDICINE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SOURCE_MEDICINE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD));
                }
                search.listHisSourceMedicineExpression.AddRange(listHisSourceMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSourceMedicineExpression.Clear();
                search.listHisSourceMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
