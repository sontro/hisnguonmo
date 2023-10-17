using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    public class HisTreatmentEndTypeExtFilterQuery : HisTreatmentEndTypeExtFilter
    {
        public HisTreatmentEndTypeExtFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_END_TYPE_EXT, bool>>> listHisTreatmentEndTypeExtExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_END_TYPE_EXT, bool>>>();

        

        internal HisTreatmentEndTypeExtSO Query()
        {
            HisTreatmentEndTypeExtSO search = new HisTreatmentEndTypeExtSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTreatmentEndTypeExtExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listHisTreatmentEndTypeExtExpression.AddRange(listHisTreatmentEndTypeExtExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTreatmentEndTypeExtExpression.Clear();
                search.listHisTreatmentEndTypeExtExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
