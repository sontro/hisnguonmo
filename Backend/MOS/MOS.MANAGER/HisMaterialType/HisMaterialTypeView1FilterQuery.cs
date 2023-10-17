using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    public class HisMaterialTypeView1FilterQuery : HisMaterialTypeView1Filter
    {
        public HisMaterialTypeView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE_1, bool>>> listVHisMaterialType1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_MATERIAL_TYPE_1, bool>>>();



        internal HisMaterialTypeSO Query()
        {
            HisMaterialTypeSO search = new HisMaterialTypeSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisMaterialType1Expression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisMaterialType1Expression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisMaterialType1Expression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisMaterialType1Expression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisMaterialType1Expression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                if (this.IS_LEAF.HasValue)
                {
                    if (this.IS_LEAF.Value)
                    {
                        search.listVHisMaterialType1Expression.Add(o => o.IS_LEAF.HasValue && o.IS_LEAF.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialType1Expression.Add(o => !o.IS_LEAF.HasValue || o.IS_LEAF.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IS_STOP_IMP.HasValue)
                {
                    if (this.IS_STOP_IMP.Value)
                    {
                        search.listVHisMaterialType1Expression.Add(o => o.IS_STOP_IMP.HasValue && o.IS_STOP_IMP.Value == MOS.UTILITY.Constant.IS_TRUE);
                    }
                    else
                    {
                        search.listVHisMaterialType1Expression.Add(o => !o.IS_STOP_IMP.HasValue || o.IS_STOP_IMP.Value != MOS.UTILITY.Constant.IS_TRUE);
                    }
                }
                if (this.IDs != null)
                {
                    search.listVHisMaterialType1Expression.Add(o => this.IDs.Contains(o.ID));
                }

                #endregion

                if (!String.IsNullOrEmpty(this.KEY_WORD))
                {
                    this.KEY_WORD = this.KEY_WORD.ToLower().Trim();
                    listVHisMaterialType1Expression.Add(o =>
                        o.MATERIAL_TYPE_CODE.ToLower().Contains(this.KEY_WORD) ||
                        o.MATERIAL_TYPE_NAME.ToLower().Contains(this.KEY_WORD) ||
                        o.CONCENTRA.ToLower().Contains(this.KEY_WORD) ||
                        o.NATIONAL_NAME.ToLower().Contains(this.KEY_WORD)
                        );
                }

                if (this.IsTree.HasValue && this.IsTree.Value)
                {
                    listVHisMaterialType1Expression.Add(o => (!o.PARENT_ID.HasValue && !this.Node.HasValue) || (o.PARENT_ID.HasValue && o.PARENT_ID.Value == this.Node.Value));
                }
                if (this.PARENT_ID.HasValue)
                {
                    listVHisMaterialType1Expression.Add(o => o.PARENT_ID.HasValue && o.PARENT_ID.Value == this.PARENT_ID.Value);
                }
                if (this.MANUFACTURER_ID.HasValue)
                {
                    listVHisMaterialType1Expression.Add(o => o.MANUFACTURER_ID.HasValue && o.MANUFACTURER_ID.Value == this.MANUFACTURER_ID.Value);
                }
                if (!String.IsNullOrEmpty(this.CN_WORD))
                {
                    this.CN_WORD = this.CN_WORD.ToLower().Trim();
                    listVHisMaterialType1Expression.Add(o => o.MATERIAL_TYPE_CODE.ToLower().Contains(this.CN_WORD) || o.MATERIAL_TYPE_NAME.ToLower().Contains(this.CN_WORD));
                }
                if (this.IS_STENT.HasValue && this.IS_STENT.Value)
                {
                    listVHisMaterialType1Expression.Add(o => o.IS_STENT.HasValue && o.IS_STENT == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_STENT.HasValue && !this.IS_STENT.Value)
                {
                    listVHisMaterialType1Expression.Add(o => !o.IS_STENT.HasValue || o.IS_STENT != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.MEMA_GROUP_ID.HasValue)
                {
                    listVHisMaterialType1Expression.Add(o => o.MEMA_GROUP_ID.HasValue && o.MEMA_GROUP_ID.Value == this.MEMA_GROUP_ID.Value);
                }
                if (this.MATERIAL_TYPE_MAP_ID.HasValue)
                {
                    listVHisMaterialType1Expression.Add(o => o.MATERIAL_TYPE_MAP_ID.HasValue && o.MATERIAL_TYPE_MAP_ID.Value == this.MATERIAL_TYPE_MAP_ID.Value);
                }
                if (this.IS_RAW_MATERIAL.HasValue && this.IS_RAW_MATERIAL.Value)
                {
                    listVHisMaterialType1Expression.Add(o => o.IS_RAW_MATERIAL.HasValue && o.IS_RAW_MATERIAL.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_RAW_MATERIAL.HasValue && !this.IS_RAW_MATERIAL.Value)
                {
                    listVHisMaterialType1Expression.Add(o => !o.IS_RAW_MATERIAL.HasValue || o.IS_RAW_MATERIAL.Value != MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_BUSINESS.HasValue && this.IS_BUSINESS.Value)
                {
                    listVHisMaterialType1Expression.Add(o => o.IS_BUSINESS.HasValue && o.IS_BUSINESS.Value == MOS.UTILITY.Constant.IS_TRUE);
                }
                if (this.IS_BUSINESS.HasValue && !this.IS_BUSINESS.Value)
                {
                    listVHisMaterialType1Expression.Add(o => !o.IS_BUSINESS.HasValue || o.IS_BUSINESS.Value != MOS.UTILITY.Constant.IS_TRUE);
                }

                search.listVHisMaterialType1Expression.AddRange(listVHisMaterialType1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
                search.DynamicColumns = this.ColumnParams;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisMaterialType1Expression.Clear();
                search.listVHisMaterialType1Expression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
