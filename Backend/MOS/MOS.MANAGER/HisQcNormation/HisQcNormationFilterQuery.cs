using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisQcNormation
{
    public class HisQcNormationFilterQuery : HisQcNormationFilter
    {
        public HisQcNormationFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<HIS_QC_NORMATION, bool>>> listHisQcNormationExpression = new List<System.Linq.Expressions.Expression<Func<HIS_QC_NORMATION, bool>>>();

        

        internal HisQcNormationSO Query()
        {
            HisQcNormationSO search = new HisQcNormationSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listHisQcNormationExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listHisQcNormationExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listHisQcNormationExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listHisQcNormationExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.QC_TYPE_ID.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.QC_TYPE_ID == this.QC_TYPE_ID.Value);
                }
                if (this.MACHINE_ID.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.MACHINE_ID == this.MACHINE_ID.Value);
                }
                if (this.MATERIAL_TYPE_ID.HasValue)
                {
                    listHisQcNormationExpression.Add(o => o.MATERIAL_TYPE_ID == this.MATERIAL_TYPE_ID.Value);
                }

                search.listHisQcNormationExpression.AddRange(listHisQcNormationExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listHisQcNormationExpression.Clear();
                search.listHisQcNormationExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
