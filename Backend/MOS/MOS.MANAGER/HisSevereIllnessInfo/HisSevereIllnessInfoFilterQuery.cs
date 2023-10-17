using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    public class HisSevereIllnessInfoFilterQuery : HisSevereIllnessInfoFilter
    {
        public HisSevereIllnessInfoFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SEVERE_ILLNESS_INFO, bool>>> listHisSevereIllnessInfoExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SEVERE_ILLNESS_INFO, bool>>>();

        

        internal HisSevereIllnessInfoSO Query()
        {
            HisSevereIllnessInfoSO search = new HisSevereIllnessInfoSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSevereIllnessInfoExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.TREATMENT_ID.HasValue)
                {
                    listHisSevereIllnessInfoExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.IS_DEATH.HasValue)
                {
                    if (this.IS_DEATH.Value)
                    {
                        listHisSevereIllnessInfoExpression.Add(o => o.IS_DEATH == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisSevereIllnessInfoExpression.Add(o => !o.IS_DEATH.HasValue || o.IS_DEATH != Constant.IS_TRUE);
                    }
                }

                search.listHisSevereIllnessInfoExpression.AddRange(listHisSevereIllnessInfoExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSevereIllnessInfoExpression.Clear();
                search.listHisSevereIllnessInfoExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
