using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMety
{
    public class HisMediContractMetyView1FilterQuery : HisMediContractMetyView1Filter
    {
        public HisMediContractMetyView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY_1, bool>>> listV1HisMediContractMetyExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEDI_CONTRACT_METY_1, bool>>>();

        

        internal HisMediContractMetySO Query()
        {
            HisMediContractMetySO search = new HisMediContractMetySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listV1HisMediContractMetyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listV1HisMediContractMetyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listV1HisMediContractMetyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listV1HisMediContractMetyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.MEDICAL_CONTRACT_ID == this.MEDICAL_CONTRACT_ID.Value);
                }
                if (this.MEDICAL_CONTRACT_IDs != null)
                {
                    listV1HisMediContractMetyExpression.Add(o => this.MEDICAL_CONTRACT_IDs.Contains(o.MEDICAL_CONTRACT_ID));
                }

                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listV1HisMediContractMetyExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                if (this.BID_MEDICINE_TYPE_ID.HasValue)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.BID_MEDICINE_TYPE_ID.HasValue && o.BID_MEDICINE_TYPE_ID.Value == this.BID_MEDICINE_TYPE_ID.Value);
                }
                if (this.BID_MEDICINE_TYPE_IDs != null)
                {
                    listV1HisMediContractMetyExpression.Add(o => o.BID_MEDICINE_TYPE_ID.HasValue && this.BID_MEDICINE_TYPE_IDs.Contains(o.BID_MEDICINE_TYPE_ID.Value));
                }

                search.listV1HisMediContractMetyExpression.AddRange(listV1HisMediContractMetyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listV1HisMediContractMetyExpression.Clear();
                search.listV1HisMediContractMetyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
