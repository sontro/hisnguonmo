using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskContract
{
    public class HisKskContractFilterQuery : HisKskContractFilter
    {
        public HisKskContractFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_KSK_CONTRACT, bool>>> listHisKskContractExpression = new List<System.Linq.Expressions.Expression<Func<HIS_KSK_CONTRACT, bool>>>();

        

        internal HisKskContractSO Query()
        {
            HisKskContractSO search = new HisKskContractSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisKskContractExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisKskContractExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisKskContractExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisKskContractExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.WORK_PLACE_ID.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.WORK_PLACE_ID == this.WORK_PLACE_ID.Value);
                }
                if (this.WORK_PLACE_IDs != null)
                {
                    listHisKskContractExpression.Add(o => this.WORK_PLACE_IDs.Contains(o.WORK_PLACE_ID));
                }

                if (this.CONTRACT_DATE_FROM.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.CONTRACT_DATE.Value >= this.CONTRACT_DATE_FROM.Value);
                }
                if (this.CONTRACT_DATE_TO.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.CONTRACT_DATE.Value <= this.CONTRACT_DATE_TO.Value);
                }
                if (this.EFFECT_DATE_FROM.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.EFFECT_DATE.Value >= this.EFFECT_DATE_FROM.Value);
                }
                if (this.EFFECT_DATE_TO.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.EFFECT_DATE.Value <= this.EFFECT_DATE_TO.Value);
                }
                if (this.EXPIRY_DATE_FROM.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.EXPIRY_DATE.Value >= this.EXPIRY_DATE_FROM.Value);
                }
                if (this.EXPIRY_DATE_TO.HasValue)
                {
                    listHisKskContractExpression.Add(o => o.EXPIRY_DATE.Value <= this.EXPIRY_DATE_TO.Value);
                }
                if (this.IS_REQUIRED_APPROVAL.HasValue)
                {
                    if (this.IS_REQUIRED_APPROVAL.Value)
                    {
                        listHisKskContractExpression.Add(o => o.IS_REQUIRED_APPROVAL.HasValue && o.IS_REQUIRED_APPROVAL.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisKskContractExpression.Add(o => !o.IS_REQUIRED_APPROVAL.HasValue || o.IS_REQUIRED_APPROVAL.Value != Constant.IS_TRUE);
                    }
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisKskContractExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.KSK_CONTRACT_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisKskContractExpression.AddRange(listHisKskContractExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisKskContractExpression.Clear();
                search.listHisKskContractExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
