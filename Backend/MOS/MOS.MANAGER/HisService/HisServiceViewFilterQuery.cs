using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisService
{
    public class HisServiceViewFilterQuery : HisServiceViewFilter
    {
        public HisServiceViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE, bool>>> listVHisServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERVICE, bool>>>();



        internal HisServiceSO Query()
        {
            HisServiceSO search = new HisServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    search.listVHisServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisServiceExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.IS_LEAF.HasValue && this.IS_LEAF.Value)
                {
                    listVHisServiceExpression.Add(o => o.IS_LEAF.HasValue && o.IS_LEAF.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_LEAF.HasValue && !this.IS_LEAF.Value)
                {
                    listVHisServiceExpression.Add(o => !o.IS_LEAF.HasValue || o.IS_LEAF.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisServiceExpression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.SERVICE_TYPE_ID));
                }
                if (!String.IsNullOrEmpty(this.SERVICE_CODE__EXACT))
                {
                    search.listVHisServiceExpression.Add(o => o.SERVICE_CODE == this.SERVICE_CODE__EXACT);
                }

                if (this.PETROLEUM_CODE != null)
                {
                    search.listVHisServiceExpression.Add(o => o.PETROLEUM_CODE == this.PETROLEUM_CODE);
                }

                if (this.PETROLEUM_NAME != null)
                {
                    search.listVHisServiceExpression.Add(o => o.PETROLEUM_NAME == this.PETROLEUM_NAME);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisServiceExpression.Add(o =>
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_BHYT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_TYPE_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.ACTIVE_INGR_BHYT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PETROLEUM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PETROLEUM_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisServiceExpression.AddRange(listVHisServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisServiceExpression.Clear();
                search.listVHisServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
