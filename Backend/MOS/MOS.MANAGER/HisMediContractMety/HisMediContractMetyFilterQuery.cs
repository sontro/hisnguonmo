using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    public class HisMediContractMetyFilterQuery : HisMediContractMetyFilter
    {
        public HisMediContractMetyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_METY, bool>>> listHisMediContractMetyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_METY, bool>>>();

        

        internal HisMediContractMetySO Query()
        {
            HisMediContractMetySO search = new HisMediContractMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMediContractMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMediContractMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMediContractMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMediContractMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.MEDICAL_CONTRACT_ID == this.MEDICAL_CONTRACT_ID.Value);
                }
                if (this.MEDICAL_CONTRACT_IDs != null)
                {
                    listHisMediContractMetyExpression.Add(o => this.MEDICAL_CONTRACT_IDs.Contains(o.MEDICAL_CONTRACT_ID));
                }

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listHisMediContractMetyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                if (this.BID_MEDICINE_TYPE_ID.HasValue)
                {
                    listHisMediContractMetyExpression.Add(o => o.BID_MEDICINE_TYPE_ID.HasValue && o.BID_MEDICINE_TYPE_ID.Value == this.BID_MEDICINE_TYPE_ID.Value);
                }
                if (this.BID_MEDICINE_TYPE_IDs != null)
                {
                    listHisMediContractMetyExpression.Add(o => o.BID_MEDICINE_TYPE_ID.HasValue && this.BID_MEDICINE_TYPE_IDs.Contains(o.BID_MEDICINE_TYPE_ID.Value));
                }

                search.listHisMediContractMetyExpression.AddRange(listHisMediContractMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMediContractMetyExpression.Clear();
                search.listHisMediContractMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
