using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqGet : BusinessBase
    {
        internal HisBcsMetyReqReqGet()
            : base()
        {

        }

        internal HisBcsMetyReqReqGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_BCS_METY_REQ_REQ> Get(HisBcsMetyReqReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqReqDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_METY_REQ_REQ GetById(long id)
        {
            try
            {
                return GetById(id, new HisBcsMetyReqReqFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BCS_METY_REQ_REQ GetById(long id, HisBcsMetyReqReqFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBcsMetyReqReqDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_METY_REQ_REQ> GetByExpMestMetyReqId(long expMestMetyReqId)
        {
            try
            {
                HisBcsMetyReqReqFilterQuery filter = new HisBcsMetyReqReqFilterQuery();
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

        internal List<HIS_BCS_METY_REQ_REQ> GetByExpMestMetyReqIds(List<long> expMestMetyReqIds)
        {
            try
            {
                HisBcsMetyReqReqFilterQuery filter = new HisBcsMetyReqReqFilterQuery();
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

        internal List<HIS_BCS_METY_REQ_REQ> GetByPreExpMestMetyReqId(long preExpMestMetyReqId)
        {
            try
            {
                HisBcsMetyReqReqFilterQuery filter = new HisBcsMetyReqReqFilterQuery();
                filter.PRE_EXP_MEST_METY_REQ_ID = preExpMestMetyReqId;
                return this.Get(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_BCS_METY_REQ_REQ> GetByPreExpMestMetyReqIds(List<long> preExpMestMetyReqIds)
        {
            try
            {
                HisBcsMetyReqReqFilterQuery filter = new HisBcsMetyReqReqFilterQuery();
                filter.PRE_EXP_MEST_METY_REQ_IDs = preExpMestMetyReqIds;
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
