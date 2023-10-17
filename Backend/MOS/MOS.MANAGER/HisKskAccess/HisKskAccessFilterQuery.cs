using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskAccess
{
    public class HisKskAccessFilterQuery : HisKskAccessFilter
    {
        public HisKskAccessFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_ACCESS, bool>>> listHisKskAccessExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_ACCESS, bool>>>();

        

        internal HisKskAccessSO Query()
        {
            HisKskAccessSO search = new HisKskAccessSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisKskAccessExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskAccessExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskAccessExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskAccessExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.KSK_CONTRACT_ID.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.KSK_CONTRACT_ID == this.KSK_CONTRACT_ID.Value);
                }
                if (this.KSK_CONTRACT_IDs != null)
                {
                    listHisKskAccessExpression.Add(o => this.KSK_CONTRACT_IDs.Contains(o.KSK_CONTRACT_ID));
                }
                if (this.EMPLOYEE_ID.HasValue)
                {
                    listHisKskAccessExpression.Add(o => o.EMPLOYEE_ID == this.EMPLOYEE_ID.Value);
                }
                if (this.EMPLOYEE_IDs != null)
                {
                    listHisKskAccessExpression.Add(o => this.EMPLOYEE_IDs.Contains(o.EMPLOYEE_ID));
                }

                search.listHisKskAccessExpression.AddRange(listHisKskAccessExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskAccessExpression.Clear();
                search.listHisKskAccessExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
