using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    public class HisImpUserTempFilterQuery : HisImpUserTempFilter
    {
        public HisImpUserTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP, bool>>> listHisImpUserTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_IMP_USER_TEMP, bool>>>();

        

        internal HisImpUserTempSO Query()
        {
            HisImpUserTempSO search = new HisImpUserTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisImpUserTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisImpUserTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisImpUserTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisImpUserTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisImpUserTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisImpUserTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisImpUserTempExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.IMP_USER_TEMP_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listHisImpUserTempExpression.AddRange(listHisImpUserTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisImpUserTempExpression.Clear();
                search.listHisImpUserTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
