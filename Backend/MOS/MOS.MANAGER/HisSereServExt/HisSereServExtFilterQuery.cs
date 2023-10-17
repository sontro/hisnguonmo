using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServExt
{
    public class HisSereServExtFilterQuery : HisSereServExtFilter
    {
        public HisSereServExtFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_EXT, bool>>> listHisSereServExtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SERE_SERV_EXT, bool>>>();

        

        internal HisSereServExtSO Query()
        {
            HisSereServExtSO search = new HisSereServExtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSereServExtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSereServExtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSereServExtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSereServExtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisSereServExtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.SERE_SERV_ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.SERE_SERV_ID == this.SERE_SERV_ID.Value);
                }
                if (this.SERE_SERV_IDs != null)
                {
                    listHisSereServExtExpression.Add(o => this.SERE_SERV_IDs.Contains(o.SERE_SERV_ID));
                }
                if (this.MACHINE_ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.MACHINE_ID.HasValue && o.MACHINE_ID.Value == this.MACHINE_ID.Value);
                }
                if (this.MACHINE_IDs != null)
                {
                    listHisSereServExtExpression.Add(o => o.MACHINE_ID.HasValue && this.MACHINE_IDs.Contains(o.MACHINE_ID.Value));
                }
                if (this.TDL_SERVICE_REQ_ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && o.TDL_SERVICE_REQ_ID.Value == this.TDL_SERVICE_REQ_ID.Value);
                }
                if (this.TDL_SERVICE_REQ_IDs != null)
                {
                    listHisSereServExtExpression.Add(o => o.TDL_SERVICE_REQ_ID.HasValue && this.TDL_SERVICE_REQ_IDs.Contains(o.TDL_SERVICE_REQ_ID.Value));
                }
                if (this.TDL_TREATMENT_ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.TDL_TREATMENT_ID.HasValue && o.TDL_TREATMENT_ID.Value == this.TDL_TREATMENT_ID.Value);
                }
                if (this.TDL_TREATMENT_IDs != null)
                {
                    listHisSereServExtExpression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TDL_TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }
                if (this.FILM_SIZE_ID.HasValue)
                {
                    listHisSereServExtExpression.Add(o => o.FILM_SIZE_ID.HasValue && o.FILM_SIZE_ID == this.FILM_SIZE_ID.Value);
                }
                if (this.FILM_SIZE_IDs != null)
                {
                    listHisSereServExtExpression.Add(o => o.FILM_SIZE_ID.HasValue && this.FILM_SIZE_IDs.Contains(o.FILM_SIZE_ID.Value));
                }

                search.listHisSereServExtExpression.AddRange(listHisSereServExtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSereServExtExpression.Clear();
                search.listHisSereServExtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
