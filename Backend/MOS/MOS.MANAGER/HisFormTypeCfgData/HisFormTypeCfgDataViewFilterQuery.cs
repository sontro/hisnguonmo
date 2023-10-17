using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfgData
{
    public class HisFormTypeCfgDataViewFilterQuery : HisFormTypeCfgDataViewFilter
    {
        public HisFormTypeCfgDataViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_FORM_TYPE_CFG_DATA, bool>>> listVHisFormTypeCfgDataExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_FORM_TYPE_CFG_DATA, bool>>>();



        internal HisFormTypeCfgDataSO Query()
        {
            HisFormTypeCfgDataSO search = new HisFormTypeCfgDataSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IDs != null)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => this.IDs.Contains(o.ID));
                }
                #endregion

                if (this.FORM_TYPE_CFG_ID.HasValue)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.FORM_TYPE_CFG_ID == this.FORM_TYPE_CFG_ID.Value);
                }
                if (this.FORM_TYPE_CFG_IDs != null)
                {
                    listVHisFormTypeCfgDataExpression.Add(o => this.FORM_TYPE_CFG_IDs.Contains(o.FORM_TYPE_CFG_ID));
                }

                if (!String.IsNullOrWhiteSpace(this.FORM_TYPE_CODE__EXACT))
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.FORM_TYPE_CODE == this.FORM_TYPE_CODE__EXACT);
                }
                if (!String.IsNullOrWhiteSpace(this.FORM_TYPE_CFG_CODE__EXACT))
                {
                    listVHisFormTypeCfgDataExpression.Add(o => o.FORM_TYPE_CFG_CODE == this.FORM_TYPE_CFG_CODE__EXACT);
                }

                if (this.HAS_VALUE.HasValue)
                {
                    if (this.HAS_VALUE.Value)
                    {
                        listVHisFormTypeCfgDataExpression.Add(o => o.VALUE != null);
                    }
                    else
                    {
                        listVHisFormTypeCfgDataExpression.Add(o => o.VALUE == null);
                    }
                }

                if (!String.IsNullOrWhiteSpace(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisFormTypeCfgDataExpression.Add(o => o.CREATOR.ToLower().Contains(this.KEY_WORD)
                        || o.DESCRIPTION.ToLower().Contains(this.KEY_WORD)
                        || o.FORM_TYPE_CFG_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.FORM_TYPE_CODE.ToLower().Contains(this.KEY_WORD)
                        || o.MODIFIER.ToLower().Contains(this.KEY_WORD)
                        || o.VALUE.ToLower().Contains(this.KEY_WORD)
                        );
                }

                search.listVHisFormTypeCfgDataExpression.AddRange(listVHisFormTypeCfgDataExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisFormTypeCfgDataExpression.Clear();
                search.listVHisFormTypeCfgDataExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
