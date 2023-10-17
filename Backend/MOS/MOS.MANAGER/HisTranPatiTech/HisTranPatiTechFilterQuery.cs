using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTech
{
    public class HisTranPatiTechFilterQuery : HisTranPatiTechFilter
    {
        public HisTranPatiTechFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TECH, bool>>> listHisTranPatiTechExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TRAN_PATI_TECH, bool>>>();

        

        internal HisTranPatiTechSO Query()
        {
            HisTranPatiTechSO search = new HisTranPatiTechSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisTranPatiTechExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisTranPatiTechExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisTranPatiTechExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisTranPatiTechExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisTranPatiTechExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisTranPatiTechExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisTranPatiTechExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.TRAN_PATI_TECH_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.TRAN_PATI_TECH_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }
                
                search.listHisTranPatiTechExpression.AddRange(listHisTranPatiTechExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisTranPatiTechExpression.Clear();
                search.listHisTranPatiTechExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
