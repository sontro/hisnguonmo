using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineService
{
    public class HisMedicineServiceFilterQuery : HisMedicineServiceFilter
    {
        public HisMedicineServiceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_SERVICE, bool>>> listHisMedicineServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_SERVICE, bool>>>();

        internal HisMedicineServiceSO Query()
        {
            HisMedicineServiceSO search = new HisMedicineServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMedicineServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicineServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicineServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicineServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listHisMedicineServiceExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }

                if (this.MEDICINE_TYPE_IDs != null && this.MEDICINE_TYPE_IDs.Count > 0)
                {
                    listHisMedicineServiceExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue && this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID.Value));
                }
                search.listHisMedicineServiceExpression.AddRange(listHisMedicineServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicineServiceExpression.Clear();
                search.listHisMedicineServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
