using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    public class HisTreatmentUnlimitViewFilterQuery : HisTreatmentUnlimitViewFilter
    {
        public HisTreatmentUnlimitViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_UNLIMIT, bool>>> listVHisTreatmentUnlimitExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_TREATMENT_UNLIMIT, bool>>>();

        

        internal HisTreatmentUnlimitSO Query()
        {
            HisTreatmentUnlimitSO search = new HisTreatmentUnlimitSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.UNLIMIT_TYPE_ID.HasValue)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => o.UNLIMIT_TYPE_ID == this.UNLIMIT_TYPE_ID.Value);
                }
                if (this.UNLIMIT_TYPE_IDs != null)
                {
                    listVHisTreatmentUnlimitExpression.Add(o => this.UNLIMIT_TYPE_IDs.Contains(o.UNLIMIT_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisTreatmentUnlimitExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.UNLIMIT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.UNLIMIT_TYPE_NAME.ToLower().Contains(this.KEY_WORD)||
                        o.REQ_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REQ_USERNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.UNLIMIT_REASON.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisTreatmentUnlimitExpression.AddRange(listVHisTreatmentUnlimitExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisTreatmentUnlimitExpression.Clear();
                search.listVHisTreatmentUnlimitExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
