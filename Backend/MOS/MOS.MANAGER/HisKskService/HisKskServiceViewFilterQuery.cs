using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    public class HisKskServiceViewFilterQuery : HisKskServiceViewFilter
    {
        public HisKskServiceViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_SERVICE, bool>>> listVHisKskServiceExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_KSK_SERVICE, bool>>>();



        internal HisKskServiceSO Query()
        {
            HisKskServiceSO search = new HisKskServiceSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisKskServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisKskServiceExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisKskServiceExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisKskServiceExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisKskServiceExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.KSK_ID.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.KSK_ID == this.KSK_ID.Value);
                }
                if (this.KSK_IDs != null)
                {
                    listVHisKskServiceExpression.Add(o => this.KSK_IDs.Contains(o.KSK_ID));
                }
                if (this.SERVICE_ID.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.SERVICE_ID == this.SERVICE_ID.Value);
                }
                if (this.SERVICE_IDs != null)
                {
                    listVHisKskServiceExpression.Add(o => this.SERVICE_IDs.Contains(o.SERVICE_ID));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listVHisKskServiceExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listVHisKskServiceExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisKskServiceExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.KSK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.KSK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisKskServiceExpression.AddRange(listVHisKskServiceExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisKskServiceExpression.Clear();
                search.listVHisKskServiceExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
