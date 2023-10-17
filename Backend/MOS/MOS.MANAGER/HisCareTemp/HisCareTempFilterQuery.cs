using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTemp
{
    public class HisCareTempFilterQuery : HisCareTempFilter
    {
        public HisCareTempFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP, bool>>> listHisCareTempExpression = new List<System.Linq.Expressions.Expression<Func<HIS_CARE_TEMP, bool>>>();

        

        internal HisCareTempSO Query()
        {
            HisCareTempSO search = new HisCareTempSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisCareTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisCareTempExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisCareTempExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisCareTempExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisCareTempExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisCareTempExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.DATA_DOMAIN_FILTER)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    listHisCareTempExpression.Add(o => o.CREATOR == loginName || o.IS_PUBLIC.HasValue && o.IS_PUBLIC.Value == MOS.UTILITY.Constant.IS_TRUE);
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisCareTempExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.AWARENESS.ToLower().Contains(this.KEY_WORD) ||
                        o.CARE_DESCRIPTION.ToLower().Contains(this.KEY_WORD) ||
                        o.CARE_TEMP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.CARE_TEMP_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.DEJECTA.ToLower().Contains(this.KEY_WORD) ||
                        o.EDUCATION.ToLower().Contains(this.KEY_WORD) ||
                        o.INSTRUCTION_DESCRIPTION.ToLower().Contains(this.KEY_WORD) ||
                        o.MUCOCUTANEOUS.ToLower().Contains(this.KEY_WORD) ||
                        o.NUTRITION.ToLower().Contains(this.KEY_WORD) ||
                        o.SANITARY.ToLower().Contains(this.KEY_WORD) ||
                        o.TUTORIAL.ToLower().Contains(this.KEY_WORD) ||
                        o.URINE.ToLower().Contains(this.KEY_WORD));
                }

                search.listHisCareTempExpression.AddRange(listHisCareTempExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisCareTempExpression.Clear();
                search.listHisCareTempExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
