using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegimenHiv
{
    public class HisRegimenHivFilterQuery : HisRegimenHivFilter
    {
        public HisRegimenHivFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_REGIMEN_HIV, bool>>> listHisRegimenHivExpression = new List<System.Linq.Expressions.Expression<Func<HIS_REGIMEN_HIV, bool>>>();

        

        internal HisRegimenHivSO Query()
        {
            HisRegimenHivSO search = new HisRegimenHivSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisRegimenHivExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisRegimenHivExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisRegimenHivExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisRegimenHivExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisRegimenHivExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisRegimenHivExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.REGIMEN_HIV_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.REGIMEN_HIV_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisRegimenHivExpression.AddRange(listHisRegimenHivExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisRegimenHivExpression.Clear();
                search.listHisRegimenHivExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
