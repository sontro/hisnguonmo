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
        internal List<L_HIS_EXP_MEST_MEDICINE_1> GetLView1(HisExpMestMedicineLView1FilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MEDICINE_1> result = DAOWorker.HisExpMestMedicineDAO.GetLView1(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE_1 GetLView1ById(long id)
        {
            try
            {
                return GetLView1ById(id, new HisExpMestMedicineLView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE_1 GetLView1ById(long id, HisExpMestMedicineLView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetLView1ById(id, filter.Query());
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
