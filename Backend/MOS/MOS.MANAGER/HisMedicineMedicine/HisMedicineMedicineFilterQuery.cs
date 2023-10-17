using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    public class HisMedicineMedicineFilterQuery : HisMedicineMedicineFilter
    {
        public HisMedicineMedicineFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MEDICINE, bool>>> listHisMedicineMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_MEDICINE, bool>>>();

        

        internal HisMedicineMedicineSO Query()
        {
            HisMedicineMedicineSO search = new HisMedicineMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMedicineMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicineMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicineMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicineMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMedicineMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_ID.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.MEDICINE_ID == this.MEDICINE_ID.Value);
                }
                if (this.MEDICINE_IDs != null)
                {
                    listHisMedicineMedicineExpression.Add(o => this.MEDICINE_IDs.Contains(o.MEDICINE_ID));
                }
                if (this.PREPARATION_MEDICINE_ID.HasValue)
                {
                    listHisMedicineMedicineExpression.Add(o => o.PREPARATION_MEDICINE_ID == this.PREPARATION_MEDICINE_ID.Value);
                }
                if (this.PREPARATION_MEDICINE_IDs != null)
                {
                    listHisMedicineMedicineExpression.Add(o => this.PREPARATION_MEDICINE_IDs.Contains(o.PREPARATION_MEDICINE_ID));
                }

                search.listHisMedicineMedicineExpression.AddRange(listHisMedicineMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicineMedicineExpression.Clear();
                search.listHisMedicineMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
