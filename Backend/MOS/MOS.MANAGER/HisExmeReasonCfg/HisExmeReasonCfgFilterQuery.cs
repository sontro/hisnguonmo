using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    public class HisExmeReasonCfgFilterQuery : HisExmeReasonCfgFilter
    {
        public HisExmeReasonCfgFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXME_REASON_CFG, bool>>> listHisExmeReasonCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXME_REASON_CFG, bool>>>();

        

        internal HisExmeReasonCfgSO Query()
        {
            HisExmeReasonCfgSO search = new HisExmeReasonCfgSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisExmeReasonCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisExmeReasonCfgExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisExmeReasonCfgExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisExmeReasonCfgExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.EXP_MEST_REASON_ID.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.EXP_MEST_REASON_ID == this.EXP_MEST_REASON_ID.Value);
                }
                if (this.PATIENT_CLASSIFY_ID.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.PATIENT_CLASSIFY_ID == this.PATIENT_CLASSIFY_ID.Value);
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listHisExmeReasonCfgExpression.Add(o => o.TREATMENT_TYPE_ID == this.TREATMENT_TYPE_ID.Value);
                }
                
                search.listHisExmeReasonCfgExpression.AddRange(listHisExmeReasonCfgExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExmeReasonCfgExpression.Clear();
                search.listHisExmeReasonCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
