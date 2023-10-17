using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMachineServMaty
{
    public class HisMachineServMatyFilterQuery : HisMachineServMatyFilter
    {
        public HisMachineServMatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MACHINE_SERV_MATY, bool>>> listHisMachineServMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MACHINE_SERV_MATY, bool>>>();

        

        internal HisMachineServMatySO Query()
        {
            HisMachineServMatySO search = new HisMachineServMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMachineServMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMachineServMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMachineServMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMachineServMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisMachineServMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MACHINE_ID.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.MACHINE_ID == this.MACHINE_ID.Value);
                }
                if (this.MACHINE_IDs != null)
                {
                    listHisMachineServMatyExpression.Add(o => this.MACHINE_IDs.Contains(o.MACHINE_ID));
                }

                if (this.SERVICE_ID.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listHisMachineServMatyExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMachineServMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMachineServMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                search.listHisMachineServMatyExpression.AddRange(listHisMachineServMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMachineServMatyExpression.Clear();
                search.listHisMachineServMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
