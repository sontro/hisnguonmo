using Inventec.Common.Logging;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizeButton
{
    public class SdaCustomizeButtonFilterQuery : SdaCustomizeButtonFilter
    {
        public SdaCustomizeButtonFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_BUTTON, bool>>> listSdaCustomizeButtonExpression = new List<System.Linq.Expressions.Expression<Func<SDA_CUSTOMIZE_BUTTON, bool>>>();

        

        internal SdaCustomizeButtonSO Query()
        {
            SdaCustomizeButtonSO search = new SdaCustomizeButtonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listSdaCustomizeButtonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listSdaCustomizeButtonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listSdaCustomizeButtonExpression.Add(o => this.IDs.Contains(o.ID));
                }

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listSdaCustomizeButtonExpression.Add(o =>
                        o.CREATOR.ToLower().Contains(this.KEY_WORD) ||
                        o.MODIFIER.ToLower().Contains(this.KEY_WORD) ||
                        o.APP_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BUTTON_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.BUTTON_PATH.ToLower().Contains(this.KEY_WORD) ||
                        o.MODULE_LINK.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (!String.IsNullOrEmpty(this.APP_CODE__EXACT))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.APP_CODE == this.APP_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.MODULE_LINK__EXACT))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.MODULE_LINK == this.MODULE_LINK__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.BRANCH_CODE__EXACT))
                {
                    listSdaCustomizeButtonExpression.Add(o => o.BRANCH_CODE == this.BRANCH_CODE__EXACT);
                }
                #endregion
                
                search.listSdaCustomizeButtonExpression.AddRange(listSdaCustomizeButtonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listSdaCustomizeButtonExpression.Clear();
                search.listSdaCustomizeButtonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
