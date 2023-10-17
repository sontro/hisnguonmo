using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttHighTech
{
    public class HisPtttHighTechFilterQuery : HisPtttHighTechFilter
    {
        public HisPtttHighTechFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_PTTT_HIGH_TECH, bool>>> listHisPtttHighTechExpression = new List<System.Linq.Expressions.Expression<Func<HIS_PTTT_HIGH_TECH, bool>>>();

        

        internal HisPtttHighTechSO Query()
        {
            HisPtttHighTechSO search = new HisPtttHighTechSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisPtttHighTechExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisPtttHighTechExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisPtttHighTechExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisPtttHighTechExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisPtttHighTechExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisPtttHighTechExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.PTTT_HIGH_TECH_CODE__EXACT))
                {
                    listHisPtttHighTechExpression.Add(o => o.PTTT_HIGH_TECH_CODE == this.PTTT_HIGH_TECH_CODE__EXACT);
                }

                search.listHisPtttHighTechExpression.AddRange(listHisPtttHighTechExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisPtttHighTechExpression.Clear();
                search.listHisPtttHighTechExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
