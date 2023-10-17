using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediContractMaty
{
    public class HisMediContractMatyFilterQuery : HisMediContractMatyFilter
    {
        public HisMediContractMatyFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_MATY, bool>>> listHisMediContractMatyExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDI_CONTRACT_MATY, bool>>>();



        internal HisMediContractMatySO Query()
        {
            HisMediContractMatySO search = new HisMediContractMatySO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMediContractMatyExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMediContractMatyExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMediContractMatyExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMediContractMatyExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDICAL_CONTRACT_ID.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.MEDICAL_CONTRACT_ID == this.MEDICAL_CONTRACT_ID.Value);
                }
                if (this.MEDICAL_CONTRACT_IDs != null)
                {
                    listHisMediContractMatyExpression.Add(o => this.MEDICAL_CONTRACT_IDs.Contains(o.MEDICAL_CONTRACT_ID));
                }

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMediContractMatyExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                if (this.BID_MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMediContractMatyExpression.Add(o => o.BID_MATERIAL_TYPE_ID.HasValue && o.BID_MATERIAL_TYPE_ID.Value == this.BID_MATERIAL_TYPE_ID.Value);
                }
                if (this.BID_MATERIAL_TYPE_IDs != null)
                {
                    listHisMediContractMatyExpression.Add(o => o.BID_MATERIAL_TYPE_ID.HasValue && this.BID_MATERIAL_TYPE_IDs.Contains(o.BID_MATERIAL_TYPE_ID.Value));
                }

                search.listHisMediContractMatyExpression.AddRange(listHisMediContractMatyExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMediContractMatyExpression.Clear();
                search.listHisMediContractMatyExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
