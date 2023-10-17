using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    public class HisKskAccessViewFilterQuery : HisKskAccessViewFilter
    {
        public HisKskAccessViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_ACCESS, bool>>> listVHisKskAccessExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_ACCESS, bool>>>();

        

        internal HisKskAccessSO Query()
        {
            HisKskAccessSO search = new HisKskAccessSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisKskAccessExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisKskAccessExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisKskAccessExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisKskAccessExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.KSK_CONTRACT_ID.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.KSK_CONTRACT_ID == this.KSK_CONTRACT_ID.Value);
                }
                if (this.KSK_CONTRACT_IDs != null)
                {
                    listVHisKskAccessExpression.Add(o => this.KSK_CONTRACT_IDs.Contains(o.KSK_CONTRACT_ID));
                }
                if (this.EMPLOYEE_ID.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.EMPLOYEE_ID == this.EMPLOYEE_ID.Value);
                }
                if (this.EMPLOYEE_IDs != null)
                {
                    listVHisKskAccessExpression.Add(o => this.EMPLOYEE_IDs.Contains(o.EMPLOYEE_ID));
                }
                if (this.EFFECT_DATE_FROM.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.EFFECT_DATE.Value >= this.EFFECT_DATE_FROM.Value);
                }
                if (this.EFFECT_DATE_TO.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.EFFECT_DATE.Value <= this.EFFECT_DATE_TO.Value);
                }
                if (this.EXPIRY_DATE_FROM.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.EXPIRY_DATE.Value >= this.EXPIRY_DATE_FROM.Value);
                }
                if (this.EXPIRY_DATE_TO.HasValue)
                {
                    listVHisKskAccessExpression.Add(o => o.EXPIRY_DATE.Value <= this.EXPIRY_DATE_TO.Value);
                }
                if (!string.IsNullOrEmpty(this.KSK_CONTRACT_CODE__EXACT))
                {
                    listVHisKskAccessExpression.Add(o => o.KSK_CONTRACT_CODE == this.KSK_CONTRACT_CODE__EXACT);
                }
                if (!string.IsNullOrEmpty(this.LOGINNAME))
                {
                    listVHisKskAccessExpression.Add(o => o.LOGINNAME == this.LOGINNAME);
                }

                search.listVHisKskAccessExpression.AddRange(listVHisKskAccessExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisKskAccessExpression.Clear();
                search.listVHisKskAccessExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
