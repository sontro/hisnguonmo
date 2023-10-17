using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    public class HisMediRecordFilterQuery : HisMediRecordFilter
    {
        public HisMediRecordFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD, bool>>> listHisMediRecordExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_RECORD, bool>>>();

        

        internal HisMediRecordSO Query()
        {
            HisMediRecordSO search = new HisMediRecordSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMediRecordExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMediRecordExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMediRecordExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMediRecordExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.VIR_STORE_YEAR__EQUAL.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.VIR_STORE_YEAR == this.VIR_STORE_YEAR__EQUAL.Value);
                }
                if (this.PROGRAM_ID.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.PROGRAM_ID.HasValue && o.PROGRAM_ID == this.PROGRAM_ID.Value);
                }
                if (this.PATIENT_ID.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.PATIENT_ID == this.PATIENT_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__END_WITH))
                {
                    listHisMediRecordExpression.Add(o => o.STORE_CODE != null && o.STORE_CODE.EndsWith(this.STORE_CODE__END_WITH));
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__START_WITH))
                {
                    listHisMediRecordExpression.Add(o => o.STORE_CODE != null && o.STORE_CODE.StartsWith(this.STORE_CODE__START_WITH));
                }
                if (this.VIR_SEED_CODE_YEAR__EQUAL.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.VIR_SEED_CODE_YEAR == this.VIR_SEED_CODE_YEAR__EQUAL.Value);
                }
                if (this.DATA_STORE_ID.HasValue)
                {
                    listHisMediRecordExpression.Add(o => o.DATA_STORE_ID.HasValue && o.DATA_STORE_ID == this.DATA_STORE_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE))
                {
                    listHisMediRecordExpression.Add(o => o.STORE_CODE == this.STORE_CODE);
                }

                search.listHisMediRecordExpression.AddRange(listHisMediRecordExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMediRecordExpression.Clear();
                search.listHisMediRecordExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
