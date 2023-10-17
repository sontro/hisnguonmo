using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPrepareMety
{
    public class HisPrepareMetyFilterQuery : HisPrepareMetyFilter
    {
        public HisPrepareMetyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_METY, bool>>> listHisPrepareMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PREPARE_METY, bool>>>();



        internal HisPrepareMetySO Query()
        {
            HisPrepareMetySO search = new HisPrepareMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisPrepareMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPrepareMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPrepareMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPrepareMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPrepareMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listHisPrepareMetyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.PREPARE_ID.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.PREPARE_ID == this.PREPARE_ID.Value);
                }
                if (this.PREPARE_IDs != null)
                {
                    listHisPrepareMetyExpression.Add(o => this.PREPARE_IDs.Contains(o.PREPARE_ID));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.TDL_TREATMENT_ID == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listHisPrepareMetyExpression.Add(o => this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID));
                }
                if (this.PREPARE_ID__NOT_EQUAL.HasValue)
                {
                    listHisPrepareMetyExpression.Add(o => o.PREPARE_ID != this.PREPARE_ID__NOT_EQUAL.Value);
                }
                if (this.HAS_APPROVAL_AMOUNT.HasValue)
                {
                    if (this.HAS_APPROVAL_AMOUNT.Value)
                    {
                        listHisPrepareMetyExpression.Add(o => o.APPROVAL_AMOUNT.HasValue);
                    }
                    else
                    {
                        listHisPrepareMetyExpression.Add(o => !o.APPROVAL_AMOUNT.HasValue);
                    }
                }

                search.listHisPrepareMetyExpression.AddRange(listHisPrepareMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPrepareMetyExpression.Clear();
                search.listHisPrepareMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
