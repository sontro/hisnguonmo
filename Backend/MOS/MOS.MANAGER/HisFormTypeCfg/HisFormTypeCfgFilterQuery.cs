using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfg
{
    public class HisFormTypeCfgFilterQuery : HisFormTypeCfgFilter
    {
        public HisFormTypeCfgFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG, bool>>> listHisFormTypeCfgExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG, bool>>>();

        

        internal HisFormTypeCfgSO Query()
        {
            HisFormTypeCfgSO search = new HisFormTypeCfgSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisFormTypeCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisFormTypeCfgExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisFormTypeCfgExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisFormTypeCfgExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisFormTypeCfgExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisFormTypeCfgExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (!String.IsNullOrWhiteSpace(this.FORM_TYPE_CFG_CODE__EXACT))
                {
                    listHisFormTypeCfgExpression.Add(o => o.FORM_TYPE_CFG_CODE == this.FORM_TYPE_CFG_CODE__EXACT);
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listHisFormTypeCfgExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.FORM_TYPE_CFG_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listHisFormTypeCfgExpression.AddRange(listHisFormTypeCfgExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisFormTypeCfgExpression.Clear();
                search.listHisFormTypeCfgExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
