using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    public class HisObeyContraindiFilterQuery : HisObeyContraindiFilter
    {
        public HisObeyContraindiFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_OBEY_CONTRAINDI, bool>>> listHisObeyContraindiExpression = new List<System.Linq.Expressions.Expression<Func<HIS_OBEY_CONTRAINDI, bool>>>();



        internal HisObeyContraindiSO Query()
        {
            HisObeyContraindiSO search = new HisObeyContraindiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisObeyContraindiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisObeyContraindiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisObeyContraindiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisObeyContraindiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                if (this.TREATMENT_ID.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.TREATMENT_ID == this.TREATMENT_ID.Value);
                }
                if (this.REQUEST_DEPARTMENT_ID.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.REQUEST_DEPARTMENT_ID == this.REQUEST_DEPARTMENT_ID.Value);
                }
                if (this.INTRUCTION_TIME.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.INTRUCTION_TIME == this.INTRUCTION_TIME.Value);
                }
                if (this.INTRUCTION_TIME_FROM.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.INTRUCTION_TIME >= this.INTRUCTION_TIME_FROM.Value);
                }
                if (this.INTRUCTION_TIME_TO.HasValue)
                {
                    listHisObeyContraindiExpression.Add(o => o.INTRUCTION_TIME <= this.INTRUCTION_TIME_TO.Value);
                }
                if (!string.IsNullOrWhiteSpace(this.SERVICE_CODE__EXACT))
                {
                    listHisObeyContraindiExpression.Add(o => o.SERVICE_CODE == this.SERVICE_CODE__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(this.SERVICE_NAME__EXACT))
                {
                    listHisObeyContraindiExpression.Add(o => o.SERVICE_NAME == this.SERVICE_NAME__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(this.REQUEST_LOGINNAME__EXACT))
                {
                    listHisObeyContraindiExpression.Add(o => o.REQUEST_LOGINNAME == this.REQUEST_LOGINNAME__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(this.SERVICE_REQ_CODE__EXACT))
                {
                    var searchPredicate = PredicateBuilder.False<HIS_OBEY_CONTRAINDI>();

                    var closureVariable = "," + SERVICE_REQ_CODE__EXACT + ",";//can khai bao bien rieng de cho vao menh de ben duoi
                    searchPredicate = searchPredicate.Or(o => (o.SERVICE_REQ_CODES != null && ("," + o.SERVICE_REQ_CODES + ",").Contains(closureVariable)));

                    listHisObeyContraindiExpression.Add(searchPredicate);
                }
                if (!string.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisObeyContraindiExpression.Add(o =>
                        o.REQUEST_LOGINNAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.SERVICE_CODE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisObeyContraindiExpression.AddRange(listHisObeyContraindiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisObeyContraindiExpression.Clear();
                search.listHisObeyContraindiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
