using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REQ_12> GetView12(HisServiceReqSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REQ_12> result = new List<V_HIS_SERVICE_REQ_12>();

            try
            {
                result = GetWorker.GetView12(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public V_HIS_SERVICE_REQ_12 GetView12ById(long id, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_12 result = null;

            try
            {
                result = GetWorker.GetView12ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_SERVICE_REQ_12 GetView12ByCode(string code, HisServiceReqSO search)
        {
            V_HIS_SERVICE_REQ_12 result = null;

            try
            {
                result = GetWorker.GetView12ByCode(code, search);
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
