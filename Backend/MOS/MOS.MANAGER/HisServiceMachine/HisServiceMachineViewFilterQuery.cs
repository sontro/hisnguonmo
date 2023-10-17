using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMachine
{
    public class HisServiceMachineViewFilterQuery : HisServiceMachineViewFilter
    {
        public HisServiceMachineViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_MACHINE, bool>>> listVHisServiceMachineExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_MACHINE, bool>>>();

        

        internal HisServiceMachineSO Query()
        {
            HisServiceMachineSO search = new HisServiceMachineSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisServiceMachineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceMachineExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceMachineExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceMachineExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceMachineExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisServiceMachineExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVHisServiceMachineExpression.AddRange(listVHisServiceMachineExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceMachineExpression.Clear();
                search.listVHisServiceMachineExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
