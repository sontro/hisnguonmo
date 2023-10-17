using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    public class HisPtttGroupBestFilterQuery : HisPtttGroupBestFilter
    {
        public HisPtttGroupBestFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PTTT_GROUP_BEST, bool>>> listHisPtttGroupBestExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_GROUP_BEST, bool>>>();

        

        internal HisPtttGroupBestSO Query()
        {
            HisPtttGroupBestSO search = new HisPtttGroupBestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPtttGroupBestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPtttGroupBestExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPtttGroupBestExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPtttGroupBestExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPtttGroupBestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BED_SERVICE_TYPE_ID.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.BED_SERVICE_TYPE_ID == this.BED_SERVICE_TYPE_ID.Value);
                }
                if (this.BED_SERVICE_TYPE_IDs != null)
                {
                    listHisPtttGroupBestExpression.Add(o => this.BED_SERVICE_TYPE_IDs.Contains(o.BED_SERVICE_TYPE_ID));
                }
                if (this.PTTT_GROUP_ID.HasValue)
                {
                    listHisPtttGroupBestExpression.Add(o => o.PTTT_GROUP_ID == this.PTTT_GROUP_ID.Value);
                }
                if (this.PTTT_GROUP_IDs != null)
                {
                    listHisPtttGroupBestExpression.Add(o => this.PTTT_GROUP_IDs.Contains(o.PTTT_GROUP_ID));
                }

                search.listHisPtttGroupBestExpression.AddRange(listHisPtttGroupBestExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPtttGroupBestExpression.Clear();
                search.listHisPtttGroupBestExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
