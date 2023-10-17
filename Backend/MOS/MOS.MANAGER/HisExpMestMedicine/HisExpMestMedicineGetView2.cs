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
        internal List<V_HIS_EXP_MEST_MEDICINE_2> GetView2(HisExpMestMedicineView2FilterQuery filter)
        {
            try
            {
                List<V_HIS_EXP_MEST_MEDICINE_2> result = DAOWorker.HisExpMestMedicineDAO.GetView2(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_2 GetView2ById(long id)
        {
            try
            {
                return GetView2ById(id, new HisExpMestMedicineView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MEDICINE_2 GetView2ById(long id, HisExpMestMedicineView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetView2ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_2> GetView2ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisExpMestMedicineView2FilterQuery filter = new HisExpMestMedicineView2FilterQuery();
                filter.IDs = ids;
                return this.GetView2(filter);
            }
            return null;
        }

        internal List<V_HIS_EXP_MEST_MEDICINE_2> GetView2ByExpMestIds(List<long> expMestIds)
        {
            try
            {
                if (expMestIds != null)
                {
                    HisExpMestMedicineView2FilterQuery filter = new HisExpMestMedicineView2FilterQuery();
                    filter.EXP_MEST_IDs = expMestIds;
                    return this.GetView2(filter);
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

        internal List<V_HIS_EXP_MEST_MEDICINE_2> GetView2ByExpMestId(long expMestId)
        {
            HisExpMestMedicineView2FilterQuery filter = new HisExpMestMedicineView2FilterQuery();
            filter.EXP_MEST_ID = expMestId;
            return this.GetView2(filter);
        }
    }
}
