using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTein
{
    partial class HisSereServTeinGet : GetBase
    {
        internal HisSereServTeinGet()
            : base()
        {

        }

        internal HisSereServTeinGet(Inventec.Core.CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_SERE_SERV_TEIN> Get(HisSereServTeinFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServTeinDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_TEIN> GetView(HisSereServTeinViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServTeinDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
        internal List<V_HIS_SERE_SERV_TEIN_1> GetView1(HisSereServTeinView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServTeinDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_TEIN GetById(long id)
        {
            try
            {
                return GetById(id, new HisSereServTeinFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_TEIN GetById(long id, HisSereServTeinFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServTeinDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_TEIN GetViewById(long id)
        {
            try
            {
                return GetViewById(id, new HisSereServTeinViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_TEIN GetViewById(long id, HisSereServTeinViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServTeinDAO.GetViewById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_TEIN> GetByTestIndexId(long id)
        {
            try
            {
                HisSereServTeinFilterQuery filter = new HisSereServTeinFilterQuery();
                filter.TEST_INDEX_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_TEIN> GetBySereServIds(List<long> ids)
        {
            try
            {
                HisSereServTeinFilterQuery filter = new HisSereServTeinFilterQuery();
                filter.SERE_SERV_IDs = ids;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_SERE_SERV_TEIN> GetBySereServId(long id)
        {
            try
            {
                HisSereServTeinFilterQuery filter = new HisSereServTeinFilterQuery();
                filter.SERE_SERV_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_TEIN> GetViewBySereServIds(List<long> sereServIds)
        {
            try
            {
                HisSereServTeinViewFilterQuery filter = new HisSereServTeinViewFilterQuery();
                filter.SERE_SERV_IDs = sereServIds;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_TEIN> GetViewByTreatmentId(long id)
        {
            try
            {
                HisSereServTeinViewFilterQuery filter = new HisSereServTeinViewFilterQuery();
                filter.TDL_TREATMENT_ID = id;
                return this.GetView(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_SERE_SERV_TEIN> GetViewByTreatmentIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisSereServTeinViewFilterQuery filter = new HisSereServTeinViewFilterQuery();
                    filter.TDL_TREATMENT_IDs = ids;
                    return this.GetView(filter);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }

        internal List<HIS_SERE_SERV_TEIN> GetByTreatmentId(long id)
        {
            try
            {
                HisSereServTeinFilterQuery filter = new HisSereServTeinFilterQuery();
                filter.TDL_TREATMENT_ID = id;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
        internal List<HIS_SERE_SERV_TEIN> GetViewByServiceReqIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisSereServTeinFilterQuery filter = new HisSereServTeinFilterQuery();
                    filter.TDL_SERVICE_REQ_IDs = ids;
                    return this.Get(filter);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }
    }
}
