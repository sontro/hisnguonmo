using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtGet : BusinessBase
    {
        internal HisBcsMatyReqDtGet()
            : base()
        {

        }

        internal HisBcsMatyReqDtGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BCS_MATY_REQ_DT> Get(HisBcsMatyReqDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqDtDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_MATY_REQ_DT GetById(long id)
        {
            try
            {
                return GetById(id, new HisBcsMatyReqDtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_MATY_REQ_DT GetById(long id, HisBcsMatyReqDtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqDtDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_MATY_REQ_DT> GetByExpMestMatyReqId(long expMestMatyReqId)
        {
            try
            {
                HisBcsMatyReqDtFilterQuery filter = new HisBcsMatyReqDtFilterQuery();
                filter.EXP_MEST_MATY_REQ_ID = expMestMatyReqId;
                return this.Get(filter);
                
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_MATY_REQ_DT> GetByExpMestMatyReqIds(List<long> expMestMatyReqIds)
        {
            try
            {
                HisBcsMatyReqDtFilterQuery filter = new HisBcsMatyReqDtFilterQuery();
                filter.EXP_MEST_MATY_REQ_IDs = expMestMatyReqIds;
                return this.Get(filter);

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_MATY_REQ_DT> GetByExpMestMaterialId(long expMestMaterialId)
        {
            try
            {
                HisBcsMatyReqDtFilterQuery filter = new HisBcsMatyReqDtFilterQuery();
                filter.EXP_MEST_MATERIAL_ID = expMestMaterialId;
                return this.Get(filter);

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_MATY_REQ_DT> GetByExpMestMaterialIds(List<long> expMestMaterialIds)
        {
            try
            {
                HisBcsMatyReqDtFilterQuery filter = new HisBcsMatyReqDtFilterQuery();
                filter.EXP_MEST_MATERIAL_IDs = expMestMaterialIds;
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
