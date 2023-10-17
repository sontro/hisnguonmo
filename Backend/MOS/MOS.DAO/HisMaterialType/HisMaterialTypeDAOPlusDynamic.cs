using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;
using MOS.DynamicDTO;

namespace MOS.DAO.HisMaterialType
{
    public partial class HisMaterialTypeDAO : EntityBase
    {
        public List<HisMaterialTypeView1DTO> GetView1Dynamic(HisMaterialTypeSO search, CommonParam param)
        {
            List<HisMaterialTypeView1DTO> result = new List<HisMaterialTypeView1DTO>();
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

        public List<HisMaterialTypeViewDTO> GetViewDynamic(HisMaterialTypeSO search, CommonParam param)
        {
            List<HisMaterialTypeViewDTO> result = new List<HisMaterialTypeViewDTO>();
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
