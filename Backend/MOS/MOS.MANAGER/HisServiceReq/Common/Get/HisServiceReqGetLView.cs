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
        internal L_HIS_SERVICE_REQ GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisServiceReqLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_SERVICE_REQ GetLViewById(long id, HisServiceReqLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetLViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_SERVICE_REQ> GetLView(HisServiceReqLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceReqDAO.GetLView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<L_HIS_SERVICE_REQ> GetLViewByIds(List<long> ids)
        {
            try
            {
                if (IsNotNullOrEmpty(ids))
                {
                    HisServiceReqLViewFilterQuery filter = new HisServiceReqLViewFilterQuery();
                    filter.IDs = ids;
                    return this.GetLView(filter);
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

        internal List<L_HIS_SERVICE_REQ> GetLViewByTreatmentId(long id)
        {
            try
            {
                HisServiceReqLViewFilterQuery filter = new HisServiceReqLViewFilterQuery();
                filter.TREATMENT_ID = id;
                return this.GetLView(filter);
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
