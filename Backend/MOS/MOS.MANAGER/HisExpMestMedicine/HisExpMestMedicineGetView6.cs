using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisMedicineBean;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMedicine
{
    partial class HisExpMestMedicineGet : GetBase
    {
        internal List<V_HIS_EXP_MEST_MEDICINE_6> GetView6(HisExpMestMedicineView6FilterQuery filter)
        {
            try
            {
                List<V_HIS_EXP_MEST_MEDICINE_6> result = DAOWorker.HisExpMestMedicineDAO.GetView6(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_6 GetView6ById(long id)
        {
            try
            {
                return GetView6ById(id, new HisExpMestMedicineView6FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_6 GetView6ById(long id, HisExpMestMedicineView6FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetView6ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_6> GetView6ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisExpMestMedicineView6FilterQuery filter = new HisExpMestMedicineView6FilterQuery();
                filter.IDs = ids;
                return this.GetView6(filter);
            }
            return null;
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_6> GetView6ByExpMestIds(List<long> expMestIds)
        {
            try
            {
                if (expMestIds != null)
                {
                    HisExpMestMedicineView6FilterQuery filter = new HisExpMestMedicineView6FilterQuery();
                    filter.EXP_MEST_IDs = expMestIds;
                    return this.GetView6(filter);
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

        internal List<V_HIS_EXP_MEST_MEDICINE_6> GetView6ByExpMestId(long expMestId)
        {
            HisExpMestMedicineView6FilterQuery filter = new HisExpMestMedicineView6FilterQuery();
            filter.EXP_MEST_ID = expMestId;
            return this.GetView6(filter);
        }
    }
}
