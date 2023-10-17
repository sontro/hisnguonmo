using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    public class HisMaterialTypeViewFilterQuery : HisMaterialTypeViewFilter
    {
        public HisMaterialTypeViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE, bool>>> listVHisMaterialTypeExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE, bool>>>();



        internal HisMaterialTypeSO Query()
        {
            HisMaterialTypeSO search = new HisMaterialTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisMaterialTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisMaterialTypeExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMaterialTypeExpression.Add(o =>
                        o.MATERIAL_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.MANUFACTURER_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_BHYT_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.HEIN_SERVICE_BHYT_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.REGISTER_NUMBER.ToLower().Contains(this.KEY_WORD) ||
                        o.PACKING_TYPE_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.IS_LEAF.HasValue)
                {
                    if (this.IS_LEAF.Value)
                    {
                        search.listVHisMaterialTypeExpression.Add(o => o.IS_LEAF.HasValue && o.IS_LEAF.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialTypeExpression.Add(o => !o.IS_LEAF.HasValue || o.IS_LEAF.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_STOP_IMP.HasValue)
                {
                    if (this.IS_STOP_IMP.Value)
                    {
                        search.listVHisMaterialTypeExpression.Add(o => o.IS_STOP_IMP.HasValue && o.IS_STOP_IMP.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialTypeExpression.Add(o => !o.IS_STOP_IMP.HasValue || o.IS_STOP_IMP.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.SERVICE_IDs != null)
                {
                    search.listVHisMaterialTypeExpression.Add(s => this.SERVICE_IDs.Contains(s.SERVICE_ID));
                }
                if (this.IDs != null)
                {
                    search.listVHisMaterialTypeExpression.Add(o => this.IDs.Contains(o.ID));
                }

                if (this.IsTree.HasValue && this.IsTree.Value)
                {
                    listVHisMaterialTypeExpression.Add(o => (!o.PARENT_ID.HasValue && !this.Node.HasValue) || (o.PARENT_ID.HasValue && o.PARENT_ID.Value == this.Node.Value));
                }
                if (!String.IsNullOrEmpty(this.CN_WORD))
                {
                    this.CN_WORD = this.CN_WORD.ToLower().Trim();
                    listVHisMaterialTypeExpression.Add(o => o.MATERIAL_TYPE_CODE.ToLower().Contains(this.CN_WORD) || o.MATERIAL_TYPE_NAME.ToLower().Contains(this.CN_WORD));
                }
                if (this.IS_BUSINESS.HasValue)
                {
                    if (this.IS_BUSINESS.Value)
                    {
                        listVHisMaterialTypeExpression.Add(o => o.IS_BUSINESS.HasValue && o.IS_BUSINESS.Value == UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        listVHisMaterialTypeExpression.Add(o => !o.IS_BUSINESS.HasValue || o.IS_BUSINESS.Value != UTILITY.Constant.IS_TRUE);
                    }
                }

                if (this.IS_DRUG_STORE.HasValue)
                {
                    if (this.IS_DRUG_STORE.Value)
                    {
                        search.listVHisMaterialTypeExpression.Add(o => o.IS_DRUG_STORE.HasValue && o.IS_DRUG_STORE.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialTypeExpression.Add(o => !o.IS_DRUG_STORE.HasValue || o.IS_DRUG_STORE.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_REUSABLE.HasValue)
                {
                    if (this.IS_REUSABLE.Value)
                    {
                        search.listVHisMaterialTypeExpression.Add(o => o.IS_REUSABLE.HasValue && o.IS_REUSABLE.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialTypeExpression.Add(o => !o.IS_REUSABLE.HasValue || o.IS_REUSABLE.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }

                search.listVHisMaterialTypeExpression.AddRange(listVHisMaterialTypeExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.DynamicColumns = this.ColumnParams;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMaterialTypeExpression.Clear();
                search.listVHisMaterialTypeExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
