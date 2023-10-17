using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    public class HisAntibioticOldRegFilterQuery : HisAntibioticOldRegFilter
    {
        public HisAntibioticOldRegFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_OLD_REG, bool>>> listHisAntibioticOldRegExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_OLD_REG, bool>>>();

        

        internal HisAntibioticOldRegSO Query()
        {
            HisAntibioticOldRegSO search = new HisAntibioticOldRegSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAntibioticOldRegExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAntibioticOldRegExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAntibioticOldRegExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAntibioticOldRegExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.ANTIBIOTIC_REQUEST_ID.HasValue)
                {
                    listHisAntibioticOldRegExpression.Add(o => o.ANTIBIOTIC_REQUEST_ID == this.ANTIBIOTIC_REQUEST_ID.Value);
                }
                
                search.listHisAntibioticOldRegExpression.AddRange(listHisAntibioticOldRegExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAntibioticOldRegExpression.Clear();
                search.listHisAntibioticOldRegExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
