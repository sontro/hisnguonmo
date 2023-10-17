using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisExpMest
{
    public partial class HisExpMestDAO : EntityBase
    {

        public List<HisExpMestView5DTO> GetView5Dynamic(HisExpMestSO search, CommonParam param)
        {
            List<HisExpMestView5DTO> result = new List<HisExpMestView5DTO>();
            try
            {
                result = GetWorker.GetView5Dynamic(search, param);
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
