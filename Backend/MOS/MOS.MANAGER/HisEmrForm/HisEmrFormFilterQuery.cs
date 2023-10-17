using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrForm
{
    public class HisEmrFormFilterQuery : HisEmrFormFilter
    {
        public HisEmrFormFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EMR_FORM, bool>>> listHisEmrFormExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EMR_FORM, bool>>>();

        

        internal HisEmrFormSO Query()
        {
            HisEmrFormSO search = new HisEmrFormSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisEmrFormExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisEmrFormExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisEmrFormExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisEmrFormExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisEmrFormExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisEmrFormExpression.Add(o => o.EMR_FORM_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.EMR_FORM_NAME.ToLower().Contains(this.KEY_WORD));
                }


                search.listHisEmrFormExpression.AddRange(listHisEmrFormExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisEmrFormExpression.Clear();
                search.listHisEmrFormExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
