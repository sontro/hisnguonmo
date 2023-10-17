using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    public class HisNoneMediServiceFilterQuery : HisNoneMediServiceFilter
    {
        public HisNoneMediServiceFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_NONE_MEDI_SERVICE, bool>>> listHisNoneMediServiceExpression = new List<System.Linq.Expressions.Expression<Func<HIS_NONE_MEDI_SERVICE, bool>>>();

        

        internal HisNoneMediServiceSO Query()
        {
            HisNoneMediServiceSO search = new HisNoneMediServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisNoneMediServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisNoneMediServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisNoneMediServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisNoneMediServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisNoneMediServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisNoneMediServiceExpression.Add(o =>
                        o.NONE_MEDI_SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.NONE_MEDI_SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisNoneMediServiceExpression.AddRange(listHisNoneMediServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisNoneMediServiceExpression.Clear();
                search.listHisNoneMediServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
