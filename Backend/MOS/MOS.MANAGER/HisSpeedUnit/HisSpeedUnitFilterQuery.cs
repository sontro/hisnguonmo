using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    public class HisSpeedUnitFilterQuery : HisSpeedUnitFilter
    {
        public HisSpeedUnitFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SPEED_UNIT, bool>>> listHisSpeedUnitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SPEED_UNIT, bool>>>();

        

        internal HisSpeedUnitSO Query()
        {
            HisSpeedUnitSO search = new HisSpeedUnitSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSpeedUnitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSpeedUnitExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSpeedUnitExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSpeedUnitExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSpeedUnitExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisSpeedUnitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisSpeedUnitExpression.AddRange(listHisSpeedUnitExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSpeedUnitExpression.Clear();
                search.listHisSpeedUnitExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
