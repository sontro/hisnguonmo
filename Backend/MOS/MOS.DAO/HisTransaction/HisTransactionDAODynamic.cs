using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisTransaction
{
    public partial class HisTransactionDAO : EntityBase
    {

        public List<HisTransactionViewDTO> GetViewDynamic(HisTransactionSO search, CommonParam param)
        {
            List<HisTransactionViewDTO> result = new List<HisTransactionViewDTO>();
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
