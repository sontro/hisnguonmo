using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqGet : BusinessBase
    {
        internal List<V_HIS_EXP_MEST_BLTY_REQ_2> GetView2(HisExpMestBltyReqView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestBltyReqDAO.GetView2(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_BLTY_REQ_2> GetViewExpMestSttIds(List<long> expMestSttIds)
        {
            try
            {
                HisExpMestBltyReqView2FilterQuery filter = new HisExpMestBltyReqView2FilterQuery();
                filter.EXP_MEST_IDs = expMestSttIds;
                return this.GetView2(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
           
        }

        internal List<V_HIS_EXP_MEST_BLTY_REQ_2> GetView2TdlTreatmentId(long? tdlTreatmentId)
        {
            try
            {
                HisExpMestBltyReqView2FilterQuery filter = new HisExpMestBltyReqView2FilterQuery();
                filter.TDL_TREATMENT_ID = tdlTreatmentId;
                return this.GetView2(filter);
            }
             catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
