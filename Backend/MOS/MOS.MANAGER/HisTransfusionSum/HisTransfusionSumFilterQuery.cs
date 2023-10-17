using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    public class HisTransfusionSumFilterQuery : HisTransfusionSumFilter
    {
        public HisTransfusionSumFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION_SUM, bool>>> listHisTransfusionSumExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRANSFUSION_SUM, bool>>>();

        

        internal HisTransfusionSumSO Query()
        {
            HisTransfusionSumSO search = new HisTransfusionSumSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTransfusionSumExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTransfusionSumExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTransfusionSumExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.TREATMENT_ID.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.TREATMENT_IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => this.TREATMENT_IDs.Contains(o.TREATMENT_ID));
                }
                if (this.EXP_MEST_BLOOD_ID.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.EXP_MEST_BLOOD_ID == this.EXP_MEST_BLOOD_ID.Value);
                }
                if (this.EXP_MEST_BLOOD_IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => this.EXP_MEST_BLOOD_IDs.Contains(o.EXP_MEST_BLOOD_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.DEPARTMENT_ID.HasValue && o.DEPARTMENT_ID.Value == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => o.DEPARTMENT_ID.HasValue && this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID.Value));
                }
                if (this.ROOM_ID.HasValue)
                {
                    listHisTransfusionSumExpression.Add(o => o.ROOM_ID.HasValue && o.ROOM_ID.Value == this.ROOM_ID.Value);
                }
                if (this.ROOM_IDs != null)
                {
                    listHisTransfusionSumExpression.Add(o => o.ROOM_ID.HasValue && this.ROOM_IDs.Contains(o.ROOM_ID.Value));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTransfusionSumExpression.Add(o => o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_LOGINNAME.ToLower().Contains(this.KEY_WORD)
                        || o.EXECUTE_USERNAME.ToLower().Contains(this.KEY_WORD)
                        || o.GROUP_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_SUB_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.ICD_TEXT.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.NOTE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisTransfusionSumExpression.AddRange(listHisTransfusionSumExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTransfusionSumExpression.Clear();
                search.listHisTransfusionSumExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
