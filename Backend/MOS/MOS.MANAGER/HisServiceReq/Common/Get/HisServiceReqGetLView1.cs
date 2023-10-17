using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqGet : GetBase
    {
        internal L_HIS_SERVICE_REQ_1 GetLView1ById(long id)
        {
            try
            {
                return GetLView1ById(id, new HisServiceReqLView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_SERVICE_REQ_1 GetLView1ById(long id, HisServiceReqLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetLView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_SERVICE_REQ_1> GetLView1(HisServiceReqLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetLView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_SERVICE_REQ_1> GetLView1ByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqLView1FilterQuery filter = new HisServiceReqLView1FilterQuery();
                    filter.IDs = ids;
                    return this.GetLView1(filter);
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

        internal List<L_HIS_SERVICE_REQ_1> GetLView1ByTreatmentId(long id)
        {
            try
            {
                HisServiceReqLView1FilterQuery filter = new HisServiceReqLView1FilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetLView1(filter);
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
