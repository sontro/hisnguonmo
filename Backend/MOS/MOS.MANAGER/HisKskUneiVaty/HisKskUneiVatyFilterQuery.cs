using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUneiVaty
{
    public class HisKskUneiVatyFilterQuery : HisKskUneiVatyFilter
    {
        public HisKskUneiVatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_UNEI_VATY, bool>>> listHisKskUneiVatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_UNEI_VATY, bool>>>();

        

        internal HisKskUneiVatySO Query()
        {
            HisKskUneiVatySO search = new HisKskUneiVatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisKskUneiVatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskUneiVatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskUneiVatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskUneiVatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.KSK_UNDER_EIGHTEEN_ID.HasValue)
                {
                    listHisKskUneiVatyExpression.Add(o => o.KSK_UNDER_EIGHTEEN_ID == this.KSK_UNDER_EIGHTEEN_ID.Value);
                }
                
                search.listHisKskUneiVatyExpression.AddRange(listHisKskUneiVatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskUneiVatyExpression.Clear();
                search.listHisKskUneiVatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
