using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisServiceReq
{
    public partial class HisServiceReqDAO : EntityBase
    {

        public List<HisServiceReqViewDTO> GetViewDynamic(HisServiceReqSO search, CommonParam param)
        {
            List<HisServiceReqViewDTO> result = new List<HisServiceReqViewDTO>();
            try
            {
                result = GetWorker.GetViewDynamic(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }
    }
}
