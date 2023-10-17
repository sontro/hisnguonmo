using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    public class HisTreatmentUnlimitFilterQuery : HisTreatmentUnlimitFilter
    {
        public HisTreatmentUnlimitFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_UNLIMIT, bool>>> listHisTreatmentUnlimitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_UNLIMIT, bool>>>();

        

        internal HisTreatmentUnlimitSO Query()
        {
            HisTreatmentUnlimitSO search = new HisTreatmentUnlimitSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTreatmentUnlimitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTreatmentUnlimitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisTreatmentUnlimitExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.UNLIMIT_TYPE_ID.HasValue)
                {
                    listHisTreatmentUnlimitExpression.Add(o => o.UNLIMIT_TYPE_ID == this.UNLIMIT_TYPE_ID.Value);
                }
                if (this.UNLIMIT_TYPE_IDs != null)
                {
                    listHisTreatmentUnlimitExpression.Add(o => this.UNLIMIT_TYPE_IDs.Contains(o.UNLIMIT_TYPE_ID));
                }

                search.listHisTreatmentUnlimitExpression.AddRange(listHisTreatmentUnlimitExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTreatmentUnlimitExpression.Clear();
                search.listHisTreatmentUnlimitExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
