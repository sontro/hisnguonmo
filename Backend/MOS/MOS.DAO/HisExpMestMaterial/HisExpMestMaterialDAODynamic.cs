using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisExpMestMaterial
{
    public partial class HisExpMestMaterialDAO : EntityBase
    {

        public List<HisExpMestMaterialDTO> GetDynamic(HisExpMestMaterialSO search, CommonParam param)
        {
            List<HisExpMestMaterialDTO> result = new List<HisExpMestMaterialDTO>();
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
