using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    public class HisDataStoreView1FilterQuery : HisDataStoreView1Filter
    {
        public HisDataStoreView1FilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<V_HIS_DATA_STORE_1, bool>>> listVHisDataStore1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_DATA_STORE_1, bool>>>();

        

        internal HisDataStoreSO Query()
        {
            HisDataStoreSO search = new HisDataStoreSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisDataStore1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisDataStore1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisDataStore1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisDataStore1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisDataStore1Expression.Add(o =>
                        o.DATA_STORE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DATA_STORE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.IsTree.HasValue && this.IsTree.Value)
                {
                    listVHisDataStore1Expression.Add(o => (!o.PARENT_ID.HasValue && !this.Node.HasValue) || (o.PARENT_ID.HasValue && o.PARENT_ID.Value == this.Node.Value));
                }
                if (this.BRANCH_ID.HasValue)
                {
                    listVHisDataStore1Expression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.BRANCH_IDs != null)
                {
                    listVHisDataStore1Expression.Add(o => this.BRANCH_IDs.Contains(o.BRANCH_ID));
                }

                search.listVHisDataStore1Expression.AddRange(listVHisDataStore1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisDataStore1Expression.Clear();
                search.listVHisDataStore1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
