using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint
{
    public class HisContactPointFilterQuery : HisContactPointFilter
    {
        public HisContactPointFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CONTACT_POINT, bool>>> listHisContactPointExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CONTACT_POINT, bool>>>();

        

        internal HisContactPointSO Query()
        {
            HisContactPointSO search = new HisContactPointSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisContactPointExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisContactPointExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisContactPointExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisContactPointExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }

                if (this.EMPLOYEE_ID.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.EMPLOYEE_ID == this.EMPLOYEE_ID.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (this.DOB.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.DOB == this.DOB.Value);
                }
                if (this.GENDER_ID.HasValue)
                {
                    listHisContactPointExpression.Add(o => o.GENDER_ID == this.GENDER_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(this.FULL_NAME_EXACT))
                {
                    listHisContactPointExpression.Add(o => o.VIR_FULL_NAME == this.FULL_NAME_EXACT);
                }

                search.listHisContactPointExpression.AddRange(listHisContactPointExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisContactPointExpression.Clear();
                search.listHisContactPointExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
