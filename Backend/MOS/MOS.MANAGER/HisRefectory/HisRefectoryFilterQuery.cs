using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    public class HisRefectoryFilterQuery : HisRefectoryFilter
    {
        public HisRefectoryFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_REFECTORY, bool>>> listHisRefectoryExpression = new List<System.Linq.Expressions.Expression<Func<HIS_REFECTORY, bool>>>();

        

        internal HisRefectorySO Query()
        {
            HisRefectorySO search = new HisRefectorySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisRefectoryExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisRefectoryExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisRefectoryExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisRefectoryExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisRefectoryExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.ROOM_ID.HasValue)
                {
                    listHisRefectoryExpression.Add(o => o.ROOM_ID == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisRefectoryExpression.Add(o => this.ROOM_IDs.Contains(o.ROOM_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.REFECTORY_CODE__EXACT))
                {
                    listHisRefectoryExpression.Add(o => o.REFECTORY_CODE == this.REFECTORY_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisRefectoryExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.REFECTORY_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.REFECTORY_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisRefectoryExpression.AddRange(listHisRefectoryExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisRefectoryExpression.Clear();
                search.listHisRefectoryExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
