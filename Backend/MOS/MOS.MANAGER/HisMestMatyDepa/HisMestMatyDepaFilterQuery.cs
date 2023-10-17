using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMatyDepa
{
    public class HisMestMatyDepaFilterQuery : HisMestMatyDepaFilter
    {
        public HisMestMatyDepaFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEST_MATY_DEPA, bool>>> listHisMestMatyDepaExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_MATY_DEPA, bool>>>();



        internal HisMestMatyDepaSO Query()
        {
            HisMestMatyDepaSO search = new HisMestMatyDepaSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMestMatyDepaExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMestMatyDepaExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMestMatyDepaExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMestMatyDepaExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listHisMestMatyDepaExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }
                if (this.DEPARTMENT_ID.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.DEPARTMENT_ID == this.DEPARTMENT_ID.Value);
                }
                if (this.DEPARTMENT_IDs != null)
                {
                    listHisMestMatyDepaExpression.Add(o => this.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID));
                }
                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.MEDI_STOCK_ID.HasValue && o.MEDI_STOCK_ID.Value == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listHisMestMatyDepaExpression.Add(o => o.MEDI_STOCK_ID.HasValue && this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID.Value));
                }
                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisMestMatyDepaExpression.Add(o => o.ID != this.ID__NOT_EQUAL.Value);
                }
                if (this.HAS_MEDI_STOCK_ID.HasValue)
                {
                    if (this.HAS_MEDI_STOCK_ID.Value)
                    {
                        listHisMestMatyDepaExpression.Add(o => o.MEDI_STOCK_ID.HasValue);
                    }
                    else
                    {
                        listHisMestMatyDepaExpression.Add(o => !o.MEDI_STOCK_ID.HasValue);
                    }
                }

                search.listHisMestMatyDepaExpression.AddRange(listHisMestMatyDepaExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMestMatyDepaExpression.Clear();
                search.listHisMestMatyDepaExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
