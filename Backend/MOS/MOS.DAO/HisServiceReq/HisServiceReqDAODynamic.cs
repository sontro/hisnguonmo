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

        public List<HisServiceReqDTO> GetDynamic(HisServiceReqSO search, CommonParam param)
        {
            List<HisServiceReqDTO> result = new List<HisServiceReqDTO>();
            try
            {
                result = GetWorker.GetDynamic(search, param);
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
