using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisObeyContraindi
{
    public class HisObeyContraindiViewFilterQuery : HisObeyContraindiViewFilter
    {
        public HisObeyContraindiViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_OBEY_CONTRAINDI, bool>>> listVHisObeyContraindiExpression = new List<System.Linq.Expressions.Expression<Func<V_HIS_OBEY_CONTRAINDI, bool>>>();

        

        internal HisObeyContraindiSO Query()
        {
            HisObeyContraindiSO search = new HisObeyContraindiSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.ID == this.ID.Value);
                }
				if (this.IDs != null)
                {
                    listVHisObeyContraindiExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    listVHisObeyContraindiExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    listVHisObeyContraindiExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    listVHisObeyContraindiExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    listVHisObeyContraindiExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion
                
                search.listVHisObeyContraindiExpression.AddRange(listVHisObeyContraindiExpression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisObeyContraindiExpression.Clear();
                search.listVHisObeyContraindiExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
