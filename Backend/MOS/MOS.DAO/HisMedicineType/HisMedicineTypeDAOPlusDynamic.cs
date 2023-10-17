using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisMedicineType
{
    public partial class HisMedicineTypeDAO : EntityBase
    {
        public List<HisMedicineTypeView1DTO> GetView1Dynamic(HisMedicineTypeSO search, CommonParam param)
        {
            List<HisMedicineTypeView1DTO> result = new List<HisMedicineTypeView1DTO>();
            try
            {
                result = GetWorker.GetView1Dynamic(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public List<HisMedicineTypeViewDTO> GetViewDynamic(HisMedicineTypeSO search, CommonParam param)
        {
            List<HisMedicineTypeViewDTO> result = new List<HisMedicineTypeViewDTO>();
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
