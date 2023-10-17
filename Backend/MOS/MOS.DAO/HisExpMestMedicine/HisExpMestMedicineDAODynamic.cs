using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisExpMestMedicine
{
    public partial class HisExpMestMedicineDAO : EntityBase
    {

        public List<HisExpMestMedicineDTO> GetDynamic(HisExpMestMedicineSO search, CommonParam param)
        {
            List<HisExpMestMedicineDTO> result = new List<HisExpMestMedicineDTO>();
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
