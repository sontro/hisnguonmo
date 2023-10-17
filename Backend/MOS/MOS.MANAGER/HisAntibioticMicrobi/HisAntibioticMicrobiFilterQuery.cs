using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticMicrobi
{
    public class HisAntibioticMicrobiFilterQuery : HisAntibioticMicrobiFilter
    {
        public HisAntibioticMicrobiFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_MICROBI, bool>>> listHisAntibioticMicrobiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_MICROBI, bool>>>();

        

        internal HisAntibioticMicrobiSO Query()
        {
            HisAntibioticMicrobiSO search = new HisAntibioticMicrobiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAntibioticMicrobiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.ANTIBIOTIC_REQUEST_ID.HasValue)
                {
                    listHisAntibioticMicrobiExpression.Add(o => o.ANTIBIOTIC_REQUEST_ID == this.ANTIBIOTIC_REQUEST_ID.Value);
                }
                
                search.listHisAntibioticMicrobiExpression.AddRange(listHisAntibioticMicrobiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAntibioticMicrobiExpression.Clear();
                search.listHisAntibioticMicrobiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
