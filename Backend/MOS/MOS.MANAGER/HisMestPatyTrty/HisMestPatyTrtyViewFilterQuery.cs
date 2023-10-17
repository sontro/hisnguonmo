using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    public class HisMestPatyTrtyViewFilterQuery : HisMestPatyTrtyViewFilter
    {
        public HisMestPatyTrtyViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PATY_TRTY, bool>>> listVHisMestPatyTrtyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_PATY_TRTY, bool>>>();

        

        internal HisMestPatyTrtySO Query()
        {
            HisMestPatyTrtySO search = new HisMestPatyTrtySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisMestPatyTrtyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisMestPatyTrtyExpression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.PATIENT_TYPE_ID.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.PATIENT_TYPE_ID == this.PATIENT_TYPE_ID.Value);
                }
                if (this.PATIENT_TYPE_IDs != null)
                {
                    listVHisMestPatyTrtyExpression.Add(o => this.PATIENT_TYPE_IDs.Contains(o.PATIENT_TYPE_ID));
                }
                if (this.TREATMENT_TYPE_ID.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.TREATMENT_TYPE_ID == this.TREATMENT_TYPE_ID.Value);
                }
                if (this.TREATMENT_TYPE_IDs != null)
                {
                    listVHisMestPatyTrtyExpression.Add(o => this.TREATMENT_TYPE_IDs.Contains(o.TREATMENT_TYPE_ID));
                }

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listVHisMestPatyTrtyExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMestPatyTrtyExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDI_STOCK_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MEDI_STOCK_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_TYPE_NAME.ToLower().Contains(this.KEY_WORD));
                }

                search.listVHisMestPatyTrtyExpression.AddRange(listVHisMestPatyTrtyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMestPatyTrtyExpression.Clear();
                search.listVHisMestPatyTrtyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
