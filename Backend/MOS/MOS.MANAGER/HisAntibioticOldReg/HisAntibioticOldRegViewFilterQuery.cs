using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    public class HisAntibioticOldRegViewFilterQuery : HisAntibioticOldRegViewFilter
    {
        public HisAntibioticOldRegViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_OLD_REG, bool>>> listVHisAntibioticOldRegExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_OLD_REG, bool>>>();

        

        internal HisAntibioticOldRegSO Query()
        {
            HisAntibioticOldRegSO search = new HisAntibioticOldRegSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAntibioticOldRegExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAntibioticOldRegExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisAntibioticOldRegExpression.AddRange(listVHisAntibioticOldRegExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAntibioticOldRegExpression.Clear();
                search.listVHisAntibioticOldRegExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
