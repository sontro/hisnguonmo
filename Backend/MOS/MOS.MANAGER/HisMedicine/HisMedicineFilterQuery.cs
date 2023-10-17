using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicine
{
    public class HisMedicineFilterQuery : HisMedicineFilter
    {
        public HisMedicineFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE, bool>>> listHisMedicineExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE, bool>>>();

        

        internal HisMedicineSO Query()
        {
            HisMedicineSO search = new HisMedicineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listHisMedicineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listHisMedicineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listHisMedicineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listHisMedicineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listHisMedicineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listHisMedicineExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listHisMedicineExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listHisMedicineExpression.Add(o => o.SUPPLIER_ID.HasValue && o.SUPPLIER_ID.Value == this.SUPPLIER_ID.Value);
                }
                if (this.BID_ID.HasValue)
                {
                    listHisMedicineExpression.Add(o => o.BID_ID.HasValue && o.BID_ID.Value == this.BID_ID.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_IMP_MEST_CODE))
                {
                    listHisMedicineExpression.Add(o => o.TDL_IMP_MEST_CODE == this.TDL_IMP_MEST_CODE);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_IMP_MEST_SUB_CODE))
                {
                    listHisMedicineExpression.Add(o => o.TDL_IMP_MEST_SUB_CODE == this.TDL_IMP_MEST_SUB_CODE);
                }
                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listHisMedicineExpression.Add(o => o.MEDICAL_CONTRACT_ID.HasValue && o.MEDICAL_CONTRACT_ID.Value == this.MEDICAL_CONTRACT_ID.Value);
                }
                
                search.listHisMedicineExpression.AddRange(listHisMedicineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicineExpression.Clear();
                search.listHisMedicineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
