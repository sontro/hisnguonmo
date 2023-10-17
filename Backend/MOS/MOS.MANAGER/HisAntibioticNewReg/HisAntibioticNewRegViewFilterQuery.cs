using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    public class HisAntibioticNewRegViewFilterQuery : HisAntibioticNewRegViewFilter
    {
        public HisAntibioticNewRegViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_NEW_REG, bool>>> listVHisAntibioticNewRegExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_NEW_REG, bool>>>();

        

        internal HisAntibioticNewRegSO Query()
        {
            HisAntibioticNewRegSO search = new HisAntibioticNewRegSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAntibioticNewRegExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.ANTIBIOTIC_REQUEST_ID.HasValue)
                {
                    listVHisAntibioticNewRegExpression.Add(o => o.ANTIBIOTIC_REQUEST_ID == this.ANTIBIOTIC_REQUEST_ID.Value);
                }
                
                search.listVHisAntibioticNewRegExpression.AddRange(listVHisAntibioticNewRegExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAntibioticNewRegExpression.Clear();
                search.listVHisAntibioticNewRegExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
