using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqGet : BusinessBase
    {
        internal HisBcsMatyReqReqGet()
            : base()
        {

        }

        internal HisBcsMatyReqReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BCS_MATY_REQ_REQ> Get(HisBcsMatyReqReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqReqDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_MATY_REQ_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisBcsMatyReqReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_MATY_REQ_REQ GetById(long id, HisBcsMatyReqReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMatyReqReqDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_MATY_REQ_REQ> GetByExpMestMatyReqId(long expMestMatyReqId)
        {
            try
            {
                HisBcsMatyReqReqFilterQuery filter = new HisBcsMatyReqReqFilterQuery();
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


        internal List<HIS_BCS_MATY_REQ_REQ> GetByExpMestMatyReqIds(List<long> expMestMatyReqIds)
        {
            try
            {
                HisBcsMatyReqReqFilterQuery filter = new HisBcsMatyReqReqFilterQuery();
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

        internal List<HIS_BCS_MATY_REQ_REQ> GetByPreExpMestMatyReqId(long preExpMestMatyReqId)
        {
            try
            {
                HisBcsMatyReqReqFilterQuery filter = new HisBcsMatyReqReqFilterQuery();
                filter.PRE_EXP_MEST_MATY_REQ_ID = preExpMestMatyReqId;
                return this.Get(filter);

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }


        internal List<HIS_BCS_MATY_REQ_REQ> GetByPreExpMestMatyReqIds(List<long> preExpMestMatyReqIds)
        {
            try
            {
                HisBcsMatyReqReqFilterQuery filter = new HisBcsMatyReqReqFilterQuery();
                filter.PRE_EXP_MEST_MATY_REQ_IDs = preExpMestMatyReqIds;
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
