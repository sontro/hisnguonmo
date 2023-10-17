using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    public class HisAntibioticNewRegFilterQuery : HisAntibioticNewRegFilter
    {
        public HisAntibioticNewRegFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_NEW_REG, bool>>> listHisAntibioticNewRegExpression = new List<System.Linq.Expressions.Expression<Func<HIS_ANTIBIOTIC_NEW_REG, bool>>>();

        

        internal HisAntibioticNewRegSO Query()
        {
            HisAntibioticNewRegSO search = new HisAntibioticNewRegSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisAntibioticNewRegExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisAntibioticNewRegExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisAntibioticNewRegExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisAntibioticNewRegExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.ANTIBIOTIC_REQUEST_ID.HasValue)
                {
                    listHisAntibioticNewRegExpression.Add(o => o.ANTIBIOTIC_REQUEST_ID == this.ANTIBIOTIC_REQUEST_ID.Value);
                }
                
                search.listHisAntibioticNewRegExpression.AddRange(listHisAntibioticNewRegExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisAntibioticNewRegExpression.Clear();
                search.listHisAntibioticNewRegExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
