using Inventec.Common.Logging;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    public class HisExpMestMetyReqLViewFilterQuery : HisExpMestMetyReqLViewFilter
    {
        public HisExpMestMetyReqLViewFilterQuery()
            : base()
        {

        }

        internal List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_METY_REQ, bool>>> listLHisExpMestMetyReqExpression = new List<System.Linq.Expressions.Expression<Func<L_HIS_EXP_MEST_METY_REQ, bool>>>();

        internal HisExpMestMetyReqSO Query()
        {
            HisExpMestMetyReqSO search = new HisExpMestMetyReqSO();
            try
            {
                if (this.HAS_CHMS_TYPE_ID.HasValue)
                {
                    if (this.HAS_CHMS_TYPE_ID.Value)
                    {
                        listLHisExpMestMetyReqExpression.Add(o => o.CHMS_TYPE_ID.HasValue);
                    }
                    else
                    {
                        listLHisExpMestMetyReqExpression.Add(o => !o.CHMS_TYPE_ID.HasValue);
                    }
                }
                if (this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID != null)
                {
                    listLHisExpMestMetyReqExpression.Add(o => o.IMP_MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID || o.MEDI_STOCK_ID == this.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID);
                }
                if (this.EXP_MEST_STT_ID != null)
                {
                    listLHisExpMestMetyReqExpression.Add(o => this.EXP_MEST_STT_ID.Value == o.EXP_MEST_STT_ID);
                }
                if (this.EXP_MEST_STT_IDs != null)
                {
                    listLHisExpMestMetyReqExpression.Add(o => this.EXP_MEST_STT_IDs.Contains(o.MEDICINE_TYPE_ID));
                }
                if (this.MEDICINE_TYPE_IDs != null)
                {
                    listLHisExpMestMetyReqExpression.Add(o => this.MEDICINE_TYPE_IDs.Contains(o.MEDICINE_TYPE_ID));
                }

                search.listLHisExpMestMetyReqExpression.AddRange(listLHisExpMestMetyReqExpression);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                search.listVHisExpMestMetyReqExpression.Clear();
                search.listVHisExpMestMetyReqExpression.Add(o => o.ID == -1);
            }
            return search;
        }
    }
}
