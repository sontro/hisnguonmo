using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal V_HIS_SERVICE_REQ_12 GetView12ById(long id)
        {
            try
            {
                return GetView12ById(id, new HisServiceReqView12FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_REQ_12 GetView12ById(long id, HisServiceReqView12FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView12ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_12> GetView12(HisServiceReqView12FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetView12(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_12> GetView12ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqView12FilterQuery filter = new HisServiceReqView12FilterQuery();
                    filter.IDs = ids;
                    return this.GetView12(filter);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERVICE_REQ_12> GetView12ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqView12FilterQuery filter = new HisServiceReqView12FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetView12(filter);
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
