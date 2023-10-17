using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServTein
{
    public class HisSereServTeinView1FilterQuery: HisSereServTeinView1Filter
    {
        public HisSereServTeinView1FilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_TEIN_1, bool>>> listVHisSereServTein1Expression = new List<System.Linq.Expressions.Expression<Func<V_HIS_SERE_SERV_TEIN_1, bool>>>();

        

        internal HisSereServTeinSO Query()
        {
            HisSereServTeinSO search = new HisSereServTeinSO();
            try
            {
                #region Abstract Base
                if (this.ID.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.ID == this.ID.Value);
                }
                if (this.IDs != null)
                {
                    search.listVHisSereServTeinExpression.Add(o => this.IDs.Contains(o.ID));
                }
                if (this.IS_ACTIVE.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.IS_ACTIVE == this.IS_ACTIVE.Value);
                }
                if (this.CREATE_TIME_FROM.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.CREATE_TIME.Value >= this.CREATE_TIME_FROM.Value);
                }
                if (this.CREATE_TIME_TO.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.CREATE_TIME.Value <= this.CREATE_TIME_TO.Value);
                }
                if (this.MODIFY_TIME_FROM.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.MODIFY_TIME.Value >= this.MODIFY_TIME_FROM.Value);
                }
                if (this.MODIFY_TIME_TO.HasValue)
                {
                    search.listVHisSereServTeinExpression.Add(o => o.MODIFY_TIME.Value <= this.MODIFY_TIME_TO.Value);
                }
                if (!String.IsNullOrEmpty(this.CREATOR))
                {
                    search.listVHisSereServTeinExpression.Add(o => o.CREATOR == this.CREATOR);
                }
                if (!String.IsNullOrEmpty(this.MODIFIER))
                {
                    search.listVHisSereServTeinExpression.Add(o => o.MODIFIER == this.MODIFIER);
                }
                if (!String.IsNullOrEmpty(this.GROUP_CODE))
                {
                    search.listVHisSereServTeinExpression.Add(o => o.GROUP_CODE == this.GROUP_CODE);
                }
                #endregion

                if (this.TEST_INDEX_IDs != null)
                {
                    search.listVHisSereServTein1Expression.Add(o => o.TEST_INDEX_ID.HasValue && this.TEST_INDEX_IDs.Contains(o.TEST_INDEX_ID.Value));
                }
                if (this.TREATMENT_IDs != null)
                {
                    search.listVHisSereServTein1Expression.Add(o => o.TDL_TREATMENT_ID.HasValue && this.TREATMENT_IDs.Contains(o.TDL_TREATMENT_ID.Value));
                }

                search.listVHisSereServTein1Expression.AddRange(listVHisSereServTein1Expression);
                search.OrderField = ORDER_FIELD;
                search.OrderDirection = ORDER_DIRECTION;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisSereServTeinExpression.Clear();
                search.listVHisSereServTeinExpression.Add(o => o.ID == NEGATIVE_ID);
            }
            return search;
        }
    }
}
