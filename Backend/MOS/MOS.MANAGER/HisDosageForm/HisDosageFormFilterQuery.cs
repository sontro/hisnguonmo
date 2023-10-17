using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    public class HisDosageFormFilterQuery : HisDosageFormFilter
    {
        public HisDosageFormFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_DOSAGE_FORM, bool>>> listHisDosageFormExpression = new List<System.Linq.Expressions.Expression<Func<HIS_DOSAGE_FORM, bool>>>();

        

        internal HisDosageFormSO Query()
        {
            HisDosageFormSO search = new HisDosageFormSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisDosageFormExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisDosageFormExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisDosageFormExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisDosageFormExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisDosageFormExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (!String.IsNullOrEmpty(this.DOSAGE_FORM_CODE))
                {
                    listHisDosageFormExpression.Add(o => o.DOSAGE_FORM_CODE == this.DOSAGE_FORM_CODE);
                }
                if (!String.IsNullOrEmpty(this.DOSAGE_FORM_NAME))
                {
                    listHisDosageFormExpression.Add(o => o.DOSAGE_FORM_NAME == this.DOSAGE_FORM_NAME);
                }
                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisDosageFormExpression.Add(o =>
                        o.DOSAGE_FORM_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.DOSAGE_FORM_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.GROUP_CODE.ToLower().Contains(this.KEY_WORD));
                }
                search.listHisDosageFormExpression.AddRange(listHisDosageFormExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisDosageFormExpression.Clear();
                search.listHisDosageFormExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
