using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    public class HisPtttGroupBestViewFilterQuery : HisPtttGroupBestViewFilter
    {
        public HisPtttGroupBestViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP_BEST, bool>>> listVHisPtttGroupBestExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_PTTT_GROUP_BEST, bool>>>();

        

        internal HisPtttGroupBestSO Query()
        {
            HisPtttGroupBestSO search = new HisPtttGroupBestSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisPtttGroupBestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisPtttGroupBestExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BED_SERVICE_TYPE_ID.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.BED_SERVICE_TYPE_ID == this.BED_SERVICE_TYPE_ID.Value);
                }
                if (this.BED_SERVICE_TYPE_IDs != null)
                {
                    listVHisPtttGroupBestExpression.Add(o => this.BED_SERVICE_TYPE_IDs.Contains(o.BED_SERVICE_TYPE_ID));
                }
                if (this.PTTT_GROUP_ID.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.PTTT_GROUP_ID == this.PTTT_GROUP_ID.Value);
                }
                if (this.PTTT_GROUP_IDs != null)
                {
                    listVHisPtttGroupBestExpression.Add(o => this.PTTT_GROUP_IDs.Contains(o.PTTT_GROUP_ID));
                }
                if (this.SERVICE_TYPE_ID.HasValue)
                {
                    listVHisPtttGroupBestExpression.Add(o => o.SERVICE_TYPE_ID == this.SERVICE_TYPE_ID.Value);
                }
                if (this.SERVICE_TYPE_IDs != null)
                {
                    listVHisPtttGroupBestExpression.Add(o => this.SERVICE_TYPE_IDs.Contains(o.SERVICE_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.HEIN_SERVICE_BHYT_CODE__EXACT))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.HEIN_SERVICE_BHYT_CODE == this.HEIN_SERVICE_BHYT_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.PTTT_GROUP_CODE__EXACT))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.PTTT_GROUP_CODE == this.PTTT_GROUP_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.SERVICE_CODE__EXACT))
                {
                    listVHisPtttGroupBestExpression.Add(o => o.SERVICE_CODE == this.SERVICE_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisPtttGroupBestExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.HEIN_SERVICE_BHYT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.HEIN_SERVICE_BHYT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.PTTT_GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.PTTT_GROUP_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisPtttGroupBestExpression.AddRange(listVHisPtttGroupBestExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisPtttGroupBestExpression.Clear();
                search.listVHisPtttGroupBestExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
