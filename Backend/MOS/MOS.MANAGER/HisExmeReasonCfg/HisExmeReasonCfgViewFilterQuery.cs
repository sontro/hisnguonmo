using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    public class HisExmeReasonCfgViewFilterQuery : HisExmeReasonCfgViewFilter
    {
        public HisExmeReasonCfgViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EXME_REASON_CFG, bool>>> listVHisExmeReasonCfgExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EXME_REASON_CFG, bool>>>();

        

        internal HisExmeReasonCfgSO Query()
        {
            HisExmeReasonCfgSO search = new HisExmeReasonCfgSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisExmeReasonCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.EXP_MEST_REASON_ID.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.EXP_MEST_REASON_ID == this.EXP_MEST_REASON_ID.Value);
                }
                if (this.PATIENT_CLASSIFY_ID.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.PATIENT_CLASSIFY_ID == this.PATIENT_CLASSIFY_ID.Value);
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.TREATMENT_TYPE_ID == this.TREATMENT_TYPE_ID.Value);
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisExmeReasonCfgExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisExmeReasonCfgExpression.Add(o => o.EXP_MEST_REASON_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.EXP_MEST_REASON_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_CLASSIFY_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_CLASSIFY_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listVHisExmeReasonCfgExpression.AddRange(listVHisExmeReasonCfgExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExmeReasonCfgExpression.Clear();
                search.listVHisExmeReasonCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
