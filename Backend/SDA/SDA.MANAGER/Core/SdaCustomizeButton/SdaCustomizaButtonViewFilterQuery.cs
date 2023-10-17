using Inventec.Common.Logging;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizaButton
{
    public class SdaCustomizaButtonViewFilterQuery : SdaCustomizaButtonViewFilter
    {
        public SdaCustomizaButtonViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZA_BUTTON, bool>>> listVSdaCustomizaButtonExpression = new List<System.Linq.Expressions.Expression<Func<V_SDA_CUSTOMIZA_BUTTON, bool>>>();

        

        internal SdaCustomizaButtonSO Query()
        {
            SdaCustomizaButtonSO search = new SdaCustomizaButtonSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVSdaCustomizaButtonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVSdaCustomizaButtonExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVSdaCustomizaButtonExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion
                
                search.listVSdaCustomizaButtonExpression.AddRange(listVSdaCustomizaButtonExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVSdaCustomizaButtonExpression.Clear();
                search.listVSdaCustomizaButtonExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
