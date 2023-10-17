using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    public class HisContactPointViewFilterQuery : HisContactPointViewFilter
    {
        public HisContactPointViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_CONTACT_POINT, bool>>> listVHisContactPointExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_CONTACT_POINT, bool>>>();

        

        internal HisContactPointSO Query()
        {
            HisContactPointSO search = new HisContactPointSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisContactPointExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisContactPointExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                if (this.CONTACT_TYPE.HasValue)
                {
                    listVHisContactPointExpression.Add(o => o.CONTACT_TYPE == this.CONTACT_TYPE.Value);
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listVHisContactPointExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listVHisContactPointExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains((long) o.DEPARTMENT_ID.Value));
                }
                if (this.CONTACT_LEVEL_FROM.HasValue)
                {
                    listVHisContactPointExpression.Add(o => o.CONTACT_LEVEL >= this.CONTACT_LEVEL_FROM.Value);
                }
                if (this.CONTACT_LEVEL_TO.HasValue)
                {
                    listVHisContactPointExpression.Add(o => o.CONTACT_LEVEL <= this.CONTACT_LEVEL_TO.Value);
                }
                if (!string.IsNullOrWhiteSpace(this.LOGINNAME__EXACT))
                {
                    listVHisContactPointExpression.Add(o => o.LOGINNAME == this.LOGINNAME__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(this.PATIENT_CODE__EXACT))
                {
                    listVHisContactPointExpression.Add(o => o.PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                
                search.listVHisContactPointExpression.AddRange(listVHisContactPointExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisContactPointExpression.Clear();
                search.listVHisContactPointExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
