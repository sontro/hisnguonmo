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

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeGet : GetBase
    {

        internal List<HisMaterialTypeView1DTO> GetView1Dynamic(HisMaterialTypeView1FilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisMaterialTypeDAO.GetView1Dynamic(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisMaterialTypeViewDTO> GetViewDynamic(HisMaterialTypeViewFilterQuery filter)
        {
            try
            {
                if (!IsNotNullOrEmpty(filter.ColumnParams))
                {
                    throw new Exception("ColumnParams Is Empty");
                }
                return DAOWorker.HisMaterialTypeDAO.GetViewDynamic(filter.Query(), param);
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
