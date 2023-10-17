using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMetyReq
{
    public partial class HisExpMestMetyReqDAO : EntityBase
    {
        public List<L_HIS_EXP_MEST_METY_REQ> GetLView(HisExpMestMetyReqSO search, CommonParam param)
        {
            List<L_HIS_EXP_MEST_METY_REQ> result = new List<L_HIS_EXP_MEST_METY_REQ>();
            try
            {
                result = GetWorker.GetLView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public L_HIS_EXP_MEST_METY_REQ GetLViewById(long id, HisExpMestMetyReqSO search)
        {
            L_HIS_EXP_MEST_METY_REQ result = null;

            try
            {
                result = GetWorker.GetLViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
