using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    public class HisMestMetyUnitViewFilterQuery : HisMestMetyUnitViewFilter
    {
        public HisMestMetyUnitViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_METY_UNIT, bool>>> listVHisMestMetyUnitExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MEST_METY_UNIT, bool>>>();



        internal HisMestMetyUnitSO Query()
        {
            HisMestMetyUnitSO search = new HisMestMetyUnitSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisMestMetyUnitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisMestMetyUnitExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisMestMetyUnitExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisMestMetyUnitExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID.Value);
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listVHisMestMetyUnitExpression.Add(o => this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID));
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listVHisMestMetyUnitExpression.Add(o => o.MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listVHisMestMetyUnitExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMestMetyUnitExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CONCENTRA.ToLower().Contains(this.KEY_WORD)
                        || o.MEDI_STOCK_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MEDI_STOCK_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MEDICINE_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_UNIT_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.SERVICE_UNIT_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisMestMetyUnitExpression.AddRange(listVHisMestMetyUnitExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMestMetyUnitExpression.Clear();
                search.listVHisMestMetyUnitExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
