using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMaty
{
    public class HisPrepareMatyFilterQuery : HisPrepareMatyFilter
    {
        public HisPrepareMatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_MATY, bool>>> listHisPrepareMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_MATY, bool>>>();



        internal HisPrepareMatySO Query()
        {
            HisPrepareMatySO search = new HisPrepareMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisPrepareMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPrepareMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPrepareMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPrepareMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPrepareMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisPrepareMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.PREPARE_ID.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.PREPARE_ID == this.PREPARE_ID.Value);
                }
                if (this.PREPARE_IDs != null)
                {
                    listHisPrepareMatyExpression.Add(o => this.PREPARE_IDs.Contains(o.PREPARE_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listHisPrepareMatyExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }
                if (this.PREPARE_ID__NOT_EQUAL.HasValue)
                {
                    listHisPrepareMatyExpression.Add(o => o.PREPARE_ID != this.PREPARE_ID__NOT_EQUAL.Value);
                }
                if (this.HAS_APPROVAL_AMOUNT.HasValue)
                {
                    if (this.HAS_APPROVAL_AMOUNT.Value)
                    {
                        listHisPrepareMatyExpression.Add(o => o.APPROVAL_AMOUNT.HasValue);
                    }
                    else
                    {
                        listHisPrepareMatyExpression.Add(o => !o.APPROVAL_AMOUNT.HasValue);
                    }
                }

                search.listHisPrepareMatyExpression.AddRange(listHisPrepareMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPrepareMatyExpression.Clear();
                search.listHisPrepareMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
