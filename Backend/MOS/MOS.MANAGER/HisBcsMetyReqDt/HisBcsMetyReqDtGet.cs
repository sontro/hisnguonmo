using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtGet : BusinessBase
    {
        internal HisBcsMetyReqDtGet()
            : base()
        {

        }

        internal HisBcsMetyReqDtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BCS_METY_REQ_DT> Get(HisBcsMetyReqDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqDtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_METY_REQ_DT GetById(long id)
        {
            try
            {
                return GetById(id, new HisBcsMetyReqDtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_METY_REQ_DT GetById(long id, HisBcsMetyReqDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqDtDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_METY_REQ_DT> GetByExpMestMetyReqId(long expMestMetyReqId)
        {
            try
            {
                HisBcsMetyReqDtFilterQuery filter = new HisBcsMetyReqDtFilterQuery();
                filter.EXP_MEST_METY_REQ_ID = expMestMetyReqId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_METY_REQ_DT> GetByExpMestMetyReqIds(List<long> expMestMetyReqIds)
        {
            try
            {
                HisBcsMetyReqDtFilterQuery filter = new HisBcsMetyReqDtFilterQuery();
                filter.EXP_MEST_METY_REQ_IDs = expMestMetyReqIds;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_METY_REQ_DT> GetByExpMestMedicineId(long expMestMedicineId)
        {
            try
            {
                HisBcsMetyReqDtFilterQuery filter = new HisBcsMetyReqDtFilterQuery();
                filter.EXP_MEST_MEDICINE_ID = expMestMedicineId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_METY_REQ_DT> GetByExpMestMedicineIds(List<long> expMestMedicineIds)
        {
            try
            {
                HisBcsMetyReqDtFilterQuery filter = new HisBcsMetyReqDtFilterQuery();
                filter.EXP_MEST_MEDICINE_IDs = expMestMedicineIds;
                return this.Get(filter);
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
