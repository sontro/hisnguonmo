using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyService
{
    public class HisBltyServiceViewFilterQuery : HisBltyServiceViewFilter
    {
        public HisBltyServiceViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_BLTY_SERVICE, bool>>> listVHisBltyServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_BLTY_SERVICE, bool>>>();



        internal HisBltyServiceSO Query()
        {
            HisBltyServiceSO search = new HisBltyServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisBltyServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisBltyServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisBltyServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisBltyServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.BLOOD_TYPE_ID.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.BLOOD_TYPE_ID == this.BLOOD_TYPE_ID.Value);
                }
                if (this.BLOOD_TYPE_IDs != null)
                {
                    listVHisBltyServiceExpression.Add(o => this.BLOOD_TYPE_IDs.Contains(o.BLOOD_TYPE_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listVHisBltyServiceExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisBltyServiceExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisBltyServiceExpression.Add(o => o.BLOOD_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BLOOD_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisBltyServiceExpression.AddRange(listVHisBltyServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisBltyServiceExpression.Clear();
                search.listVHisBltyServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
