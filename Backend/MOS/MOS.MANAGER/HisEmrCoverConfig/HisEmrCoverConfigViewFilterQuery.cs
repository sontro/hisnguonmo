using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    public class HisEmrCoverConfigViewFilterQuery : HisEmrCoverConfigViewFilter
    {
        public HisEmrCoverConfigViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_CONFIG, bool>>> listVHisEmrCoverConfigExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_EMR_COVER_CONFIG, bool>>>();

        

        internal HisEmrCoverConfigSO Query()
        {
            HisEmrCoverConfigSO search = new HisEmrCoverConfigSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisEmrCoverConfigExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisEmrCoverConfigExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisEmrCoverConfigExpression.Add(o => o.DEPARTMENT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.DEPARTMENT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.EMR_COVER_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.EMR_COVER_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ROOM_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.TREATMENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD));
                }


                search.listVHisEmrCoverConfigExpression.AddRange(listVHisEmrCoverConfigExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisEmrCoverConfigExpression.Clear();
                search.listVHisEmrCoverConfigExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
