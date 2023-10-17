using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    public class HisSesePtttMethodFilterQuery : HisSesePtttMethodFilter
    {
        public HisSesePtttMethodFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SESE_PTTT_METHOD, bool>>> listHisSesePtttMethodExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SESE_PTTT_METHOD, bool>>>();

        

        internal HisSesePtttMethodSO Query()
        {
            HisSesePtttMethodSO search = new HisSesePtttMethodSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSesePtttMethodExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSesePtttMethodExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSesePtttMethodExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSesePtttMethodExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TDL_SERE_SERV_ID.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.TDL_SERE_SERV_ID == this.TDL_SERE_SERV_ID.Value);
                }
                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.TDL_SERVICE_REQ_ID == this.TDL_SERVICE_REQ_ID.Value);
                }
                if (this.SERE_SERV_PTTT_ID.HasValue)
                {
                    listHisSesePtttMethodExpression.Add(o => o.SERE_SERV_PTTT_ID == this.SERE_SERV_PTTT_ID.Value);
                }
                if (this.SERE_SERV_PTTT_IDs != null)
                {
                    listHisSesePtttMethodExpression.Add(o => this.SERE_SERV_PTTT_IDs.Contains(o.SERE_SERV_PTTT_ID));
                }

                search.listHisSesePtttMethodExpression.AddRange(listHisSesePtttMethodExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSesePtttMethodExpression.Clear();
                search.listHisSesePtttMethodExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
