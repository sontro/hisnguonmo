using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceHein
{
    public class HisServiceHeinViewFilterQuery : HisServiceHeinViewFilter
    {
        public HisServiceHeinViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_HEIN, bool>>> listVHisServiceHeinExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE_HEIN, bool>>>();

        

        internal HisServiceHeinSO Query()
        {
            HisServiceHeinSO search = new HisServiceHeinSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisServiceHeinExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisServiceHeinExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisServiceHeinExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisServiceHeinExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisServiceHeinExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisServiceHeinExpression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisServiceHeinExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisServiceHeinExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisServiceHeinExpression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.SERVICE_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.BRANCH_CODE__EXACT))
                {
                    listVHisServiceHeinExpression.Add(o => o.BRANCH_CODE == this.BRANCH_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_CODE__EXACT))
                {
                    listVHisServiceHeinExpression.Add(o => o.SERVICE_CODE == this.SERVICE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_TYPE_CODE__EXACT))
                {
                    listVHisServiceHeinExpression.Add(o => o.SERVICE_TYPE_CODE == this.SERVICE_TYPE_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceHeinExpression.Add(o => o.BRANCH_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.BRANCH_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_CODES.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisServiceHeinExpression.AddRange(listVHisServiceHeinExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceHeinExpression.Clear();
                search.listVHisServiceHeinExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
