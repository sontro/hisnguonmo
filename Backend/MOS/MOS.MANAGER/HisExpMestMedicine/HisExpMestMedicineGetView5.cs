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
        internal List<V_HIS_EXP_MEST_MEDICINE_5> GetView5(HisExpMestMedicineView5FilterQuery filter)
        {
            try
            {
                List<V_HIS_EXP_MEST_MEDICINE_5> result = DAOWorker.HisExpMestMedicineDAO.GetView5(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_5 GetView5ById(long id)
        {
            try
            {
                return GetView5ById(id, new HisExpMestMedicineView5FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_5 GetView5ById(long id, HisExpMestMedicineView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetView5ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_5> GetView5ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisExpMestMedicineView5FilterQuery filter = new HisExpMestMedicineView5FilterQuery();
                filter.IDs = ids;
                return this.GetView5(filter);
            }
            return null;
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_5> GetView5ByExpMestIds(List<long> expMestIds)
        {
            try
            {
                if (expMestIds != null)
                {
                    HisExpMestMedicineView5FilterQuery filter = new HisExpMestMedicineView5FilterQuery();
                    filter.EXP_MEST_IDs = expMestIds;
                    return this.GetView5(filter);
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

        internal List<V_HIS_EXP_MEST_MEDICINE_5> GetView5ByExpMestId(long expMestId)
        {
            HisExpMestMedicineView5FilterQuery filter = new HisExpMestMedicineView5FilterQuery();
            filter.EXP_MEST_ID = expMestId;
            return this.GetView5(filter);
        }
    }
}
