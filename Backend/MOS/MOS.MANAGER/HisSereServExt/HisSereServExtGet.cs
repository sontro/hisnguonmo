using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtGet : BusinessBase
    {
        internal HisSereServExtGet()
            : base()
        {

        }

        internal HisSereServExtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_EXT> Get(HisSereServExtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServExtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_EXT GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServExtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_EXT GetById(long id, HisSereServExtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServExtDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_EXT> GetBySereServIds(List<long> sereServIds)
        {
            if (sereServIds != null)
            {
                HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                return this.Get(filter);
            }
            return null;
        }

        internal HIS_SERE_SERV_EXT GetBySereServId(long sereServId)
        {
            HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
            filter.SERE_SERV_ID = sereServId;
            List<HIS_SERE_SERV_EXT> sereServExts = this.Get(filter);
            return IsNotNullOrEmpty(sereServExts) ? sereServExts[0] : null;
        }

        internal List<HIS_SERE_SERV_EXT> GetByMachineId(long machineId)
        {
            HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
            filter.MACHINE_ID = machineId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV_EXT> GetByServiceReqId(long serviceReqId)
        {
            HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
            filter.TDL_SERVICE_REQ_ID = serviceReqId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV_EXT> GetByTreatmentId(long treatmentId)
        {
            HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
            filter.TDL_TREATMENT_ID = treatmentId;
            return this.Get(filter);
        }

        internal List<HIS_SERE_SERV_EXT> GetByTreatmentIds(List<long> treatmentIds)
        {
            if (treatmentIds!=null)
            {
                HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                filter.TDL_TREATMENT_IDs = treatmentIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_EXT> GetByServiceReqIds(List<long> serviceReqIds)
        {
            if (serviceReqIds != null)
            {
                HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
                filter.TDL_SERVICE_REQ_IDs = serviceReqIds;
                return this.Get(filter);
            }
            return null;
        }

        internal List<HIS_SERE_SERV_EXT> GetByFilmSizeId(long filmSizeId)
        {
            HisSereServExtFilterQuery filter = new HisSereServExtFilterQuery();
            filter.FILM_SIZE_ID = filmSizeId;
            return this.Get(filter);
        }
    }
}
