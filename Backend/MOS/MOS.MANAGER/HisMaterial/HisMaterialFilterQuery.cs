using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterial
{
    public class HisMaterialFilterQuery : HisMaterialFilter
    {
        public HisMaterialFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL, bool>>> listHisMaterialExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MATERIAL, bool>>>();

        

        internal HisMaterialSO Query()
        {
            HisMaterialSO search = new HisMaterialSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listHisMaterialExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listHisMaterialExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listHisMaterialExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listHisMaterialExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listHisMaterialExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMaterialExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listHisMaterialExpression.Add(o => o.SUPPLIER_ID.HasValue && o.SUPPLIER_ID.Value == this.SUPPLIER_ID.Value);
                }
                if (this.BID_ID.HasValue)
                {
                    listHisMaterialExpression.Add(o => o.BID_ID.HasValue && o.BID_ID.Value == this.BID_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMaterialExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_IMP_MEST_CODE))
                {
                    listHisMaterialExpression.Add(o => o.TDL_IMP_MEST_CODE == this.TDL_IMP_MEST_CODE);
                }
                if (!String.IsNullOrWhiteSpace(this.TDL_IMP_MEST_SUB_CODE))
                {
                    listHisMaterialExpression.Add(o => o.TDL_IMP_MEST_SUB_CODE == this.TDL_IMP_MEST_SUB_CODE);
                }
                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listHisMaterialExpression.Add(o => o.MEDICAL_CONTRACT_ID.HasValue && o.MEDICAL_CONTRACT_ID.Value == this.MEDICAL_CONTRACT_ID.Value);
                }

                search.listHisMaterialExpression.AddRange(listHisMaterialExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMaterialExpression.Clear();
                search.listHisMaterialExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
