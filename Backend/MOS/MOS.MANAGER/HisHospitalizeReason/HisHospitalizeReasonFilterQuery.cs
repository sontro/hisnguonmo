using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHospitalizeReason
{
    public class HisHospitalizeReasonFilterQuery : HisHospitalizeReasonFilter
    {
        public HisHospitalizeReasonFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_HOSPITALIZE_REASON, bool>>> listHisHospitalizeReasonExpression = new List<System.Linq.Expressions.Expression<Func<HIS_HOSPITALIZE_REASON, bool>>>();

        

        internal HisHospitalizeReasonSO Query()
        {
            HisHospitalizeReasonSO search = new HisHospitalizeReasonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisHospitalizeReasonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisHospitalizeReasonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisHospitalizeReasonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisHospitalizeReasonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisHospitalizeReasonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisHospitalizeReasonExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.HOSPITALIZE_REASON_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.HOSPITALIZE_REASON_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisHospitalizeReasonExpression.AddRange(listHisHospitalizeReasonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisHospitalizeReasonExpression.Clear();
                search.listHisHospitalizeReasonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
