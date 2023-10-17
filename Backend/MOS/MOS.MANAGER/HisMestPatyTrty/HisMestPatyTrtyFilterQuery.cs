using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    public class HisMestPatyTrtyFilterQuery : HisMestPatyTrtyFilter
    {
        public HisMestPatyTrtyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEST_PATY_TRTY, bool>>> listHisMestPatyTrtyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_PATY_TRTY, bool>>>();



        internal HisMestPatyTrtySO Query()
        {
            HisMestPatyTrtySO search = new HisMestPatyTrtySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMestPatyTrtyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMestPatyTrtyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMestPatyTrtyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMestPatyTrtyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listHisMestPatyTrtyExpression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listHisMestPatyTrtyExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.TREATMENT_TYPE_ID == this.TREATMENT_TYPE_ID.Value);
                }
                if (this.TREATMENT_TYPE_IDs != null)
                {
                    listHisMestPatyTrtyExpression.Add(o => this.TREATMENT_TYPE_IDs.Contains(o.TREATMENT_TYPE_ID));
                }

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisMestPatyTrtyExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }

                search.listHisMestPatyTrtyExpression.AddRange(listHisMestPatyTrtyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMestPatyTrtyExpression.Clear();
                search.listHisMestPatyTrtyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
