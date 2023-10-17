using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBodyPart
{
    public class HisBodyPartFilterQuery : HisBodyPartFilter
    {
        public HisBodyPartFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_BODY_PART, bool>>> listHisBodyPartExpression = new List<System.Linq.Expressions.Expression<Func<HIS_BODY_PART, bool>>>();

        

        internal HisBodyPartSO Query()
        {
            HisBodyPartSO search = new HisBodyPartSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisBodyPartExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisBodyPartExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisBodyPartExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisBodyPartExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisBodyPartExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.BODY_PART_CODE__EXACT))
                {
                    listHisBodyPartExpression.Add(o => o.BODY_PART_CODE == this.BODY_PART_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisBodyPartExpression.Add(o => 
                        o.BODY_PART_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BODY_PART_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                search.listHisBodyPartExpression.AddRange(listHisBodyPartExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisBodyPartExpression.Clear();
                search.listHisBodyPartExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
