using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineInteractive
{
    public class HisMedicineInteractiveFilterQuery : HisMedicineInteractiveFilter
    {
        public HisMedicineInteractiveFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_INTERACTIVE, bool>>> listHisMedicineInteractiveExpression = new List<System.Linq.Expressions.Expression<Func<HIS_MEDICINE_INTERACTIVE, bool>>>();

        

        internal HisMedicineInteractiveSO Query()
        {
            HisMedicineInteractiveSO search = new HisMedicineInteractiveSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisMedicineInteractiveExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisMedicineInteractiveExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisMedicineInteractiveExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisMedicineInteractiveExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisMedicineInteractiveExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listHisMedicineInteractiveExpression.AddRange(listHisMedicineInteractiveExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisMedicineInteractiveExpression.Clear();
                search.listHisMedicineInteractiveExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
