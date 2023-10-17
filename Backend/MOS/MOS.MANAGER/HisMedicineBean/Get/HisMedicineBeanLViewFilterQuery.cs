using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean
{
    public class HisMedicineBeanLViewFilterQuery : HisMedicineBeanLViewFilter
    {
        public HisMedicineBeanLViewFilterQuery()
            : base()
        {

        }
        
        internal List<System.Linq.Expressions.Expression<Func<L_HIS_MEDICINE_BEAN, bool>>> listLHisMedicineBeanExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_MEDICINE_BEAN, bool>>>();

        

        internal HisMedicineBeanSO Query()
        {
            HisMedicineBeanSO search = new HisMedicineBeanSO();
            try
            {
                if (this.ID.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    listLHisMedicineBeanExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }                

                if (this.MEDI_STOCK_ID.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.MEDI_STOCK_ID == this.MEDI_STOCK_ID);
                }
                if (this.MEDICINE_TYPE_ID.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.TDL_MEDICINE_TYPE_ID == this.MEDICINE_TYPE_ID);
                }
                if (this.MEDICINE_TYPE_IS_ACTIVE.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.MEDICINE_TYPE_IS_ACTIVE.HasValue && o.MEDICINE_TYPE_IS_ACTIVE.Value == this.MEDICINE_TYPE_IS_ACTIVE.Value);
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listLHisMedicineBeanExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.TDL_MEDICINE_TYPE_ID));
                }
                if (this.MEDI_STOCK_IDs != null)
                {
                    listLHisMedicineBeanExpression.Add(o => o.MEDI_STOCK_ID.HasValue && this.MEDI_STOCK_IDs.Contains(o.MEDI_STOCK_ID.Value));
                }
                if (this.MEDICINE_IS_ACTIVE.HasValue)
                {
                    listLHisMedicineBeanExpression.Add(o => o.TDL_MEDICINE_IS_ACTIVE.HasValue && o.TDL_MEDICINE_IS_ACTIVE.Value == this.MEDICINE_IS_ACTIVE.Value);
                }
                if (this.IS_BUSINESS.HasValue)
                {
                    if (this.IS_BUSINESS.Value)
                    {
                        listLHisMedicineBeanExpression.Add(o => o.IS_BUSINESS.HasValue && o.IS_BUSINESS.Value == Constant.IS_TRUE);
                    }
                    else
                    {
                        listLHisMedicineBeanExpression.Add(o => !o.IS_BUSINESS.HasValue || o.IS_BUSINESS.Value != Constant.IS_TRUE);
                    }
                }

                search.listLHisMedicineBeanExpression.AddRange(listLHisMedicineBeanExpression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listLHisMedicineBeanExpression.Clear();
                search.listLHisMedicineBeanExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
