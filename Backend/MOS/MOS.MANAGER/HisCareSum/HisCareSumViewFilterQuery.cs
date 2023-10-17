using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareSum
{
    public class HisCareSumViewFilterQuery : HisCareSumViewFilter
    {
        public HisCareSumViewFilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CARE_SUM, bool>>> listVHisCareSumExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CARE_SUM, bool>>>();

        

        internal HisCareSumSO Query()
        {
            HisCareSumSO search = new HisCareSumSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisCareSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisCareSumExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisCareSumExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisCareSumExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisCareSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listVHisCareSumExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisCareSumExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_MAIN_TEXT.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_SUB_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ICD_TEXT.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisCareSumExpression.AddRange(listVHisCareSumExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisCareSumExpression.Clear();
                search.listVHisCareSumExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
