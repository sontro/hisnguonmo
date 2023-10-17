using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    public class HisTranPatiTempFilterQuery : HisTranPatiTempFilter
    {
        public HisTranPatiTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TEMP, bool>>> listHisTranPatiTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TEMP, bool>>>();

        

        internal HisTranPatiTempSO Query()
        {
            HisTranPatiTempSO search = new HisTranPatiTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTranPatiTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTranPatiTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTranPatiTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTranPatiTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.TRAN_PATI_REASON_ID.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.TRAN_PATI_REASON_ID == this.TRAN_PATI_REASON_ID.Value);
                }
                if (this.TRAN_PATI_REASON_IDs != null)
                {
                    listHisTranPatiTempExpression.Add(o => this.TRAN_PATI_REASON_IDs.Contains(o.TRAN_PATI_REASON_ID));
                }
                if (this.TRAN_PATI_FORM_ID.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.TRAN_PATI_FORM_ID == this.TRAN_PATI_FORM_ID.Value);
                }
                if (this.TRAN_PATI_FORM_IDs != null)
                {
                    listHisTranPatiTempExpression.Add(o => this.TRAN_PATI_FORM_IDs.Contains(o.TRAN_PATI_FORM_ID));
                }
                if (this.TRAN_PATI_TECH_ID.HasValue)
                {
                    listHisTranPatiTempExpression.Add(o => o.TRAN_PATI_TECH_ID == this.TRAN_PATI_TECH_ID.Value);
                }
                if (this.TRAN_PATI_TECH_IDs != null)
                {
                    listHisTranPatiTempExpression.Add(o => o.TRAN_PATI_TECH_ID.HasValue && this.TRAN_PATI_TECH_IDs.Contains(o.TRAN_PATI_TECH_ID.Value));
                }
                if (this.IS_PUBLIC.HasValue)
                {
                    if (this.IS_PUBLIC.Value)
                    {
                        listHisTranPatiTempExpression.Add(o => o.IS_PUBLIC.HasValue && o.IS_PUBLIC.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listHisTranPatiTempExpression.Add(o => !o.IS_PUBLIC.HasValue || o.IS_PUBLIC.Value != Constant.IS_TRUE);
                    }
                }
                if (!String.IsNullOrEmpty(this.MEDI_ORG_CODE__EXACT))
                {
                    listHisTranPatiTempExpression.Add(o => o.MEDI_ORG_CODE == this.MEDI_ORG_CODE__EXACT);
                }
                if (!String.IsNullOrEmpty(this.TRAN_PATI_TEMP_CODE__EXACT))
                {
                    listHisTranPatiTempExpression.Add(o => o.TRAN_PATI_TEMP_CODE == this.TRAN_PATI_TEMP_CODE__EXACT);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTranPatiTempExpression.Add(o =>
                        o.TRAN_PATI_TEMP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TRAN_PATI_TEMP_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }


                search.listHisTranPatiTempExpression.AddRange(listHisTranPatiTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTranPatiTempExpression.Clear();
                search.listHisTranPatiTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
