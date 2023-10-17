using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestTemplate
{
    public class HisExpMestTemplateFilterQuery : HisExpMestTemplateFilter
    {
        public HisExpMestTemplateFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_EXP_MEST_TEMPLATE, bool>>> listHisExpMestTemplateExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EXP_MEST_TEMPLATE, bool>>>();



        internal HisExpMestTemplateSO Query()
        {
            HisExpMestTemplateSO search = new HisExpMestTemplateSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisExpMestTemplateExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower();
                    listHisExpMestTemplateExpression.Add(o =>
                        o.APP_CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.APP_MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.EXP_MEST_TEMPLATE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.EXP_MEST_TEMPLATE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }
                if (this.IS_PUBLIC.HasValue)
                {
                    search.listHisExpMestTemplateExpression.Add(o => o.IS_PUBLIC == this.IS_PUBLIC.Value);
                }

                search.listHisExpMestTemplateExpression.AddRange(listHisExpMestTemplateExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisExpMestTemplateExpression.Clear();
                search.listHisExpMestTemplateExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
