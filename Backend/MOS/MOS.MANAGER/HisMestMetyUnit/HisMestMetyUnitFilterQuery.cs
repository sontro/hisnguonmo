using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyUnit
{
    public class HisMestMetyUnitFilterQuery : HisMestMetyUnitFilter
    {
        public HisMestMetyUnitFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEST_METY_UNIT, bool>>> listHisMestMetyUnitExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEST_METY_UNIT, bool>>>();



        internal HisMestMetyUnitSO Query()
        {
            HisMestMetyUnitSO search = new HisMestMetyUnitSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisMestMetyUnitExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMestMetyUnitExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMestMetyUnitExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMestMetyUnitExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMestMetyUnitExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisMestMetyUnitExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisMestMetyUnitExpression.AddRange(listHisMestMetyUnitExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMestMetyUnitExpression.Clear();
                search.listHisMestMetyUnitExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
