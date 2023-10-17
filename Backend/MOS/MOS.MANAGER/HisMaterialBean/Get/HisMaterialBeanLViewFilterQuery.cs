using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean
{
    public class HisMaterialBeanLViewFilterQuery : HisMaterialBeanLViewFilter
    {
        public HisMaterialBeanLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_MATERIAL_BEAN, bool>>> listLHisMaterialBeanExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_MATERIAL_BEAN, bool>>>();



        internal HisMaterialBeanSO Query()
        {
            HisMaterialBeanSO search = new HisMaterialBeanSO();
            try
            {
                if (this.ID.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listLHisMaterialBeanExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID);
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.TDL_MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID);
                }
                if (this.MATERIAL_TYPE_IS_ACTIVE.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.MATERIAL_TYPE_IS_ACTIVE.HasValue && o.MATERIAL_TYPE_IS_ACTIVE.Value == this.MATERIAL_TYPE_IS_ACTIVE.Value);
                }
                if (this.MATERIAL_IS_ACTIVE.HasValue)
                {
                    listLHisMaterialBeanExpression.Add(o => o.TDL_MATERIAL_IS_ACTIVE.HasValue && o.TDL_MATERIAL_IS_ACTIVE.Value == this.MATERIAL_IS_ACTIVE.Value);
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listLHisMaterialBeanExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.TDL_MATERIAL_TYPE_ID));
                }

                if (this.MEDI_STOCK_IDs != null)
                {
                    listLHisMaterialBeanExpression.Add(o => o.MEDI_STOCK_ID.HasValue && this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID.Value));
                }
                if (this.IS_BUSINESS.HasValue)
                {
                    if (this.IS_BUSINESS.Value)
                    {
                        listLHisMaterialBeanExpression.Add(o => o.IS_BUSINESS.HasValue && o.IS_BUSINESS.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listLHisMaterialBeanExpression.Add(o => !o.IS_BUSINESS.HasValue || o.IS_BUSINESS.Value != Constant.IS_TRUE);
                    }
                }
                search.listLHisMaterialBeanExpression.AddRange(listLHisMaterialBeanExpression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisMaterialBeanExpression.Clear();
                search.listLHisMaterialBeanExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
