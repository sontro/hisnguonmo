using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    public class HisFormTypeCfgDataFilterQuery : HisFormTypeCfgDataFilter
    {
        public HisFormTypeCfgDataFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG_DATA, bool>>> listHisFormTypeCfgDataExpression = new List<System.Linq.Expressions.Expression<Func<HIS_FORM_TYPE_CFG_DATA, bool>>>();



        internal HisFormTypeCfgDataSO Query()
        {
            HisFormTypeCfgDataSO search = new HisFormTypeCfgDataSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listHisFormTypeCfgDataExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listHisFormTypeCfgDataExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.FORM_TYPE_CFG_ID.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.FORM_TYPE_CFG_ID == this.FORM_TYPE_CFG_ID.Value);
                }
                if (this.FORM_TYPE_CFG_IDs != null)
                {
                    listHisFormTypeCfgDataExpression.Add(o => this.FORM_TYPE_CFG_IDs.Contains(o.FORM_TYPE_CFG_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.FORM_TYPE_CODE__EXACT))
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.FORM_TYPE_CODE == this.FORM_TYPE_CODE__EXACT);
                }

                if (this.ID__NOT_EQUAL.HasValue)
                {
                    listHisFormTypeCfgDataExpression.Add(o => o.ID != this.ID__NOT_EQUAL);
                }

                if (this.HAS_VALUE.HasValue)
                {
                    if (this.HAS_VALUE.Value)
                    {
                        listHisFormTypeCfgDataExpression.Add(o => o.VALUE != null);
                    }
                    else
                    {
                        listHisFormTypeCfgDataExpression.Add(o => o.VALUE == null);
                    }
                }

                search.listHisFormTypeCfgDataExpression.AddRange(listHisFormTypeCfgDataExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisFormTypeCfgDataExpression.Clear();
                search.listHisFormTypeCfgDataExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
