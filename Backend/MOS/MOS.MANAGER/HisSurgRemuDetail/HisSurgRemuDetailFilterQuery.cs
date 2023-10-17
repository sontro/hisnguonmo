using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    public class HisSurgRemuDetailFilterQuery : HisSurgRemuDetailFilter
    {
        public HisSurgRemuDetailFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMU_DETAIL, bool>>> listHisSurgRemuDetailExpression = new List<System.Linq.Expressions.Expression<Func<HIS_SURG_REMU_DETAIL, bool>>>();

        

        internal HisSurgRemuDetailSO Query()
        {
            HisSurgRemuDetailSO search = new HisSurgRemuDetailSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisSurgRemuDetailExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisSurgRemuDetailExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisSurgRemuDetailExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisSurgRemuDetailExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion


                if (this.ID_NOT_EQUAL.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.ID != this.ID_NOT_EQUAL.Value);
                }
                if (this.EXECUTE_ROLE_ID.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.EXECUTE_ROLE_ID == this.EXECUTE_ROLE_ID.Value);
                }
                if (this.EXECUTE_ROLE_IDs != null)
                {
                    listHisSurgRemuDetailExpression.Add(o => this.EXECUTE_ROLE_IDs.Contains(o.EXECUTE_ROLE_ID));
                }
                if (this.SURG_REMUNERATION_ID.HasValue)
                {
                    listHisSurgRemuDetailExpression.Add(o => o.SURG_REMUNERATION_ID == this.SURG_REMUNERATION_ID.Value);
                }
                if (this.SURG_REMUNERATION_IDs != null)
                {
                    listHisSurgRemuDetailExpression.Add(o => this.SURG_REMUNERATION_IDs.Contains(o.SURG_REMUNERATION_ID));
                } 
                
                search.listHisSurgRemuDetailExpression.AddRange(listHisSurgRemuDetailExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisSurgRemuDetailExpression.Clear();
                search.listHisSurgRemuDetailExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
