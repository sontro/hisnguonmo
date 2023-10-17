using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestMedicine
{
    partial class HisImpMestMedicineGet : GetBase
    {

        internal List<V_HIS_IMP_MEST_MEDICINE_4> GetView4(HisImpMestMedicineView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestMedicineDAO.GetView4(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_MEDICINE_4 GetView4ById(long id)
        {
            try
            {
                return GetView4ById(id, new HisImpMestMedicineView4FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_IMP_MEST_MEDICINE_4 GetView4ById(long id, HisImpMestMedicineView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisImpMestMedicineDAO.GetView4ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_IMP_MEST_MEDICINE_4> GetView4ByImpMestId(long id)
        {
            try
            {
                HisImpMestMedicineView4FilterQuery filter = new HisImpMestMedicineView4FilterQuery();
                filter.IMP_MEST_ID = id;
                return this.GetView4(filter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_IMP_MEST_MEDICINE_4> GetView4ByImpMestIds(List<long> ids)
        {
            try
            {
                if (ids != null)
                {
                    HisImpMestMedicineView4FilterQuery filter = new HisImpMestMedicineView4FilterQuery();
                    filter.IMP_MEST_IDs = ids;
                    return this.GetView4(filter);
                }
                return null;
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
