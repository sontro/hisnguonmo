using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalContract
{
    public class HisMedicalContractFilterQuery : HisMedicalContractFilter
    {
        public HisMedicalContractFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_CONTRACT, bool>>> listHisMedicalContractExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICAL_CONTRACT, bool>>>();



        internal HisMedicalContractSO Query()
        {
            HisMedicalContractSO search = new HisMedicalContractSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMedicalContractExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicalContractExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicalContractExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicalContractExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.BID_ID.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.BID_ID.HasValue && o.BID_ID.Value == this.BID_ID.Value);
                }
                if (this.BID_IDs != null)
                {
                    listHisMedicalContractExpression.Add(o => o.BID_ID.HasValue && this.BID_IDs.Contains(o.BID_ID.Value));
                }
                if (this.SUPPLIER_ID.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.SUPPLIER_ID == this.SUPPLIER_ID.Value);
                }
                if (this.SUPPLIER_IDs != null)
                {
                    listHisMedicalContractExpression.Add(o => this.SUPPLIER_IDs.Contains(o.SUPPLIER_ID));
                }
                if (this.DOCUMENT_SUPPLIER_ID.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.DOCUMENT_SUPPLIER_ID.HasValue && o.DOCUMENT_SUPPLIER_ID.Value == this.DOCUMENT_SUPPLIER_ID.Value);
                }
                if (this.DOCUMENT_SUPPLIER_IDs != null)
                {
                    listHisMedicalContractExpression.Add(o => o.DOCUMENT_SUPPLIER_ID.HasValue && this.DOCUMENT_SUPPLIER_IDs.Contains(o.DOCUMENT_SUPPLIER_ID.Value));
                }
                if (this.CREATE_DATE_FROM.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.VIR_CREATE_DATE.Value >= this.CREATE_DATE_FROM.Value);
                }
                if (this.CREATE_DATE_TO.HasValue)
                {
                    listHisMedicalContractExpression.Add(o => o.VIR_CREATE_DATE.Value <= this.CREATE_DATE_TO.Value);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisMedicalContractExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICAL_CONTRACT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICAL_CONTRACT_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.NOTE.ToLower().Contains(this.KEY_WORD)
                        || o.VENTURE_AGREENING.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.MEDICAL_CONTRACT_CODEs != null)
                {
                    listHisMedicalContractExpression.Add(o => this.MEDICAL_CONTRACT_CODEs.Contains(o.MEDICAL_CONTRACT_CODE));
                }

                search.listHisMedicalContractExpression.AddRange(listHisMedicalContractExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicalContractExpression.Clear();
                search.listHisMedicalContractExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
