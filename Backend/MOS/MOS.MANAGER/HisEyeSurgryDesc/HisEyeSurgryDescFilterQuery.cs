using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEyeSurgryDesc
{
    public class HisEyeSurgryDescFilterQuery : HisEyeSurgryDescFilter
    {
        public HisEyeSurgryDescFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EYE_SURGRY_DESC, bool>>> listHisEyeSurgryDescExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EYE_SURGRY_DESC, bool>>>();

        

        internal HisEyeSurgryDescSO Query()
        {
            HisEyeSurgryDescSO search = new HisEyeSurgryDescSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEyeSurgryDescExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEyeSurgryDescExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEyeSurgryDescExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEyeSurgryDescExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEyeSurgryDescExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisEyeSurgryDescExpression.AddRange(listHisEyeSurgryDescExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEyeSurgryDescExpression.Clear();
                search.listHisEyeSurgryDescExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
