using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    public class HisLocationStoreFilterQuery : HisLocationStoreFilter
    {
        public HisLocationStoreFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_LOCATION_STORE, bool>>> listHisLocationStoreExpression = new List<System.Linq.Expressions.Expression<Func<HIS_LOCATION_STORE, bool>>>();

        

        internal HisLocationStoreSO Query()
        {
            HisLocationStoreSO search = new HisLocationStoreSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisLocationStoreExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisLocationStoreExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisLocationStoreExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisLocationStoreExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisLocationStoreExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.LOCATION_STORE_CODE))
                {
                    listHisLocationStoreExpression.Add(o => o.LOCATION_STORE_CODE == this.LOCATION_STORE_CODE);
                }
                if (!String.IsNullOrEmpty(this.LOCATION_STORE_NAME))
                {
                    listHisLocationStoreExpression.Add(o => o.LOCATION_STORE_NAME == this.LOCATION_STORE_NAME);
                }
                if (!string.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisLocationStoreExpression.Add(o =>
                        o.LOCATION_STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.LOCATION_STORE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listHisLocationStoreExpression.AddRange(listHisLocationStoreExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisLocationStoreExpression.Clear();
                search.listHisLocationStoreExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
