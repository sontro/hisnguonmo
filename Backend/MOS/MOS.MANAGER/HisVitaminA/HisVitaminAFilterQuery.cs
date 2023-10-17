using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    public class HisVitaminAFilterQuery : HisVitaminAFilter
    {
        public HisVitaminAFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_VITAMIN_A, bool>>> listHisVitaminAExpression = new List<System.Linq.Expressions.Expression<Func<HIS_VITAMIN_A, bool>>>();



        internal HisVitaminASO Query()
        {
            HisVitaminASO search = new HisVitaminASO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisVitaminAExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisVitaminAExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisVitaminAExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisVitaminAExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisVitaminAExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.BRANCH_ID.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.BRANCH_ID == this.BRANCH_ID.Value);
                }
                if (this.EXP_MEST_ID.HasValue)
                {
                    listHisVitaminAExpression.Add(o => o.EXP_MEST_ID.HasValue && o.EXP_MEST_ID.Value == this.EXP_MEST_ID.Value);
                }
                if (this.HasExecuteTime.HasValue)
                {
                    if (this.HasExecuteTime.Value)
                    {
                        listHisVitaminAExpression.Add(o => o.EXECUTE_TIME.HasValue);
                    }
                    else
                    {
                        listHisVitaminAExpression.Add(o => !o.EXECUTE_TIME.HasValue);
                    }
                }
                if (this.HasMedicineTypeId.HasValue)
                {
                    if (this.HasMedicineTypeId.Value)
                    {
                        listHisVitaminAExpression.Add(o => o.MEDICINE_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listHisVitaminAExpression.Add(o => !o.MEDICINE_TYPE_ID.HasValue);
                    }
                }

                search.listHisVitaminAExpression.AddRange(listHisVitaminAExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisVitaminAExpression.Clear();
                search.listHisVitaminAExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
