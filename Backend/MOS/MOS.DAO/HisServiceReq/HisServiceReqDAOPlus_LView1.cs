using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {
        public List<L_HIS_SERVICE_REQ_1> GetLView1(HisServiceReqSO search, CommonParam param)
        {
            List<L_HIS_SERVICE_REQ_1> result = new List<L_HIS_SERVICE_REQ_1>();

            try
            {
                result = GetWorker.GetLView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }
        
        public L_HIS_SERVICE_REQ_1 GetLView1ById(long id, HisServiceReqSO search)
        {
            L_HIS_SERVICE_REQ_1 result = null;

            try
            {
                result = GetWorker.GetLView1ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public L_HIS_SERVICE_REQ_1 GetLView1ByCode(string code, HisServiceReqSO search)
        {
            L_HIS_SERVICE_REQ_1 result = null;

            try
            {
                result = GetWorker.GetLView1ByCode(code, search);
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
