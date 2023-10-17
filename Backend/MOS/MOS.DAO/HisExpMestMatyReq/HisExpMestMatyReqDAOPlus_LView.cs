using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMatyReq
{
    public partial class HisExpMestMatyReqDAO : EntityBase
    {
        public List<L_HIS_EXP_MEST_MATY_REQ> GetLView(HisExpMestMatyReqSO search, CommonParam param)
        {
            List<L_HIS_EXP_MEST_MATY_REQ> result = new List<L_HIS_EXP_MEST_MATY_REQ>();
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

        public L_HIS_EXP_MEST_MATY_REQ GetLViewById(long id, HisExpMestMatyReqSO search)
        {
            L_HIS_EXP_MEST_MATY_REQ result = null;

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
