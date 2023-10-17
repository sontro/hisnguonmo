using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {
        public List<L_HIS_SERVICE_REQ> GetLView(HisServiceReqSO search, CommonParam param)
        {
            List<L_HIS_SERVICE_REQ> result = new List<L_HIS_SERVICE_REQ>();

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
        
        public L_HIS_SERVICE_REQ GetLViewById(long id, HisServiceReqSO search)
        {
            L_HIS_SERVICE_REQ result = null;

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

        public L_HIS_SERVICE_REQ GetLViewByCode(string code, HisServiceReqSO search)
        {
            L_HIS_SERVICE_REQ result = null;

            try
            {
                result = GetWorker.GetLViewByCode(code, search);
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
