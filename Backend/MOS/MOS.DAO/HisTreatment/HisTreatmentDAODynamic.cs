using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisTreatment
{
    public partial class HisTreatmentDAO : EntityBase
    {

        public List<HisTreatmentDTO> GetDynamic(HisTreatmentSO search, CommonParam param)
        {
            List<HisTreatmentDTO> result = new List<HisTreatmentDTO>();
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
