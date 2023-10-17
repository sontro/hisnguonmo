using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineType
{
    partial class HisMedicineTypeGet : GetBase
    {

        internal List<HisMedicineTypeView1DTO> GetView1Dynamic(HisMedicineTypeView1FilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisMedicineTypeDAO.GetView1Dynamic(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisMedicineTypeViewDTO> GetViewDynamic(HisMedicineTypeViewFilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisMedicineTypeDAO.GetViewDynamic(filter.Query(), param);
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
