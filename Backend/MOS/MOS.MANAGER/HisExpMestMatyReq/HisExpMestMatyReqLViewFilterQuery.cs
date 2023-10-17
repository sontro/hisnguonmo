using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    public class HisExpMestMatyReqLViewFilterQuery : HisExpMestMatyReqLViewFilter
    {
        public HisExpMestMatyReqLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATY_REQ, bool>>> listLHisExpMestMatyReqExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_MATY_REQ, bool>>>();

        internal HisExpMestMatyReqSO Query()
        {
            HisExpMestMatyReqSO search = new HisExpMestMatyReqSO();
            try
            {
                if (this.HAS_CHMS_TYPE_ID.HasValue)
                {
                    if (this.HAS_CHMS_TYPE_ID.Value)
                    {
                        listLHisExpMestMatyReqExpression.Add(o => o.CHMS_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listLHisExpMestMatyReqExpression.Add(o => !o.CHMS_TYPE_ID.HasValue);
                    }
                }
                if (this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID != null)
                {
                    listLHisExpMestMatyReqExpression.Add(o => o.IMP_MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID || o.MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listLHisExpMestMatyReqExpression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listLHisExpMestMatyReqExpression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.EXP_MEST_STT_ID));
                }
                if (this.MATERIAL_TYPE_IDs != null)
                {
                    listLHisExpMestMatyReqExpression.Add(o => this.MATERIAL_TYPE_IDs.Contains(o.MATERIAL_TYPE_ID));
                }

                search.listLHisExpMestMatyReqExpression.AddRange(listLHisExpMestMatyReqExpression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMatyReqExpression.Clear();
                search.listVHisExpMestMatyReqExpression.Add(o => o.ID == -1);
            }
            return search;
        }
    }
}
