using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    public class HisAntibioticMicrobiViewFilterQuery : HisAntibioticMicrobiViewFilter
    {
        public HisAntibioticMicrobiViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_MICROBI, bool>>> listVHisAntibioticMicrobiExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_ANTIBIOTIC_MICROBI, bool>>>();

        

        internal HisAntibioticMicrobiSO Query()
        {
            HisAntibioticMicrobiSO search = new HisAntibioticMicrobiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisAntibioticMicrobiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisAntibioticMicrobiExpression.AddRange(listVHisAntibioticMicrobiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisAntibioticMicrobiExpression.Clear();
                search.listVHisAntibioticMicrobiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
