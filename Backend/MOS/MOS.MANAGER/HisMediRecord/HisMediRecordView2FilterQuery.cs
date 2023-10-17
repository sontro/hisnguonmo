using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord
{
    public class HisMediRecordView2FilterQuery : HisMediRecordView2Filter
    {
        public HisMediRecordView2FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_2, bool>>> listVHisMediRecord2Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_RECORD_2, bool>>>();



        internal HisMediRecordSO Query()
        {
            HisMediRecordSO search = new HisMediRecordSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMediRecord2Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMediRecord2Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMediRecord2Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMediRecord2Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.DATA_STORE_ID.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.DATA_STORE_ID.HasValue && o.DATA_STORE_ID.Value == this.DATA_STORE_ID.Value);
                }
                if (this.DATA_STORE_IDs != null)
                {
                    listVHisMediRecord2Expression.Add(o => o.DATA_STORE_ID.HasValue && this.DATA_STORE_IDs.Contains(o.DATA_STORE_ID.Value));
                }
                if (this.STORE_TIME_FROM.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME.Value >= this.STORE_TIME_FROM.Value);
                }
                if (this.STORE_TIME_TO.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.STORE_TIME.HasValue && o.STORE_TIME.Value <= this.STORE_TIME_TO.Value);
                }
                if (this.PROGRAM_ID.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.PROGRAM_ID.HasValue && o.PROGRAM_ID.Value == this.PROGRAM_ID.Value);
                }
                if (this.PROGRAM_IDs != null)
                {
                    listVHisMediRecord2Expression.Add(o => o.PROGRAM_ID.HasValue && this.PROGRAM_IDs.Contains(o.PROGRAM_ID.Value));
                }
                if (this.MEDI_RECORD_TYPE_ID.HasValue)
                {
                    listVHisMediRecord2Expression.Add(o => o.MEDI_RECORD_TYPE_ID.HasValue && o.MEDI_RECORD_TYPE_ID.Value == this.MEDI_RECORD_TYPE_ID.Value);
                }
                if (this.MEDI_RECORD_TYPE_IDs != null)
                {
                    listVHisMediRecord2Expression.Add(o => o.MEDI_RECORD_TYPE_ID.HasValue && this.MEDI_RECORD_TYPE_IDs.Contains(o.MEDI_RECORD_TYPE_ID.Value));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMediRecord2Expression.Add(o =>
                        o.VIR_PATIENT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.TREATMENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.PATIENT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.STORE_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (!String.IsNullOrEmpty(this.TREATMENT_CODE))
                {
                    string code = "," + this.TREATMENT_CODE + ",";
                    listVHisMediRecord2Expression.Add(o => ("," + o.TREATMENT_CODE + ",").Contains(code));
                }
                if (!String.IsNullOrEmpty(this.STORE_CODE__EXACT))
                {
                    listVHisMediRecord2Expression.Add(o => o.STORE_CODE != null && o.STORE_CODE == this.STORE_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.PATIENT_CODE__EXACT))
                {
                    listVHisMediRecord2Expression.Add(o => o.PATIENT_CODE == this.PATIENT_CODE__EXACT);
                }
                if (this.IS_NOT_STORED.HasValue)
                {
                    if (this.IS_NOT_STORED.Value)
                    {
                        listVHisMediRecord2Expression.Add(o => o.IS_NOT_STORED.HasValue && o.IS_NOT_STORED.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisMediRecord2Expression.Add(o => !o.IS_NOT_STORED.HasValue || o.IS_NOT_STORED.Value != Constant.IS_TRUE);
                    }
                }

                search.listVHisMediRecord2Expression.AddRange(listVHisMediRecord2Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMediRecord2Expression.Clear();
                search.listVHisMediRecord2Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
