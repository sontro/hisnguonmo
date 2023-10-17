using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTrackingTemp
{
    public class HisTrackingTempFilterQuery : HisTrackingTempFilter
    {
        public HisTrackingTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRACKING_TEMP, bool>>> listHisTrackingTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRACKING_TEMP, bool>>>();

        

        internal HisTrackingTempSO Query()
        {
            HisTrackingTempSO search = new HisTrackingTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTrackingTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTrackingTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTrackingTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTrackingTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTrackingTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTrackingTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTrackingTempExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.CONTENT.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDICAL_INSTRUCTION.ToLower().Contains(this.KEY_WORD) ||
                        o.TRACKING_TEMP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TRACKING_TEMP_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    listHisTrackingTempExpression.Add(o => o.CREATOR == loginName || o.IS_PUBLIC.HasValue && o.IS_PUBLIC.Value == MOS.UTILITY.Constant.IS_TRUE);
                }

                search.listHisTrackingTempExpression.AddRange(listHisTrackingTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTrackingTempExpression.Clear();
                search.listHisTrackingTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
