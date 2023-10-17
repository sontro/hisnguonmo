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
        internal List<L_HIS_EXP_MEST_MEDICINE_2> GetLView2(HisExpMestMedicineLView2FilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MEDICINE_2> result = DAOWorker.HisExpMestMedicineDAO.GetLView2(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE_2 GetLView2ById(long id)
        {
            try
            {
                return GetLView2ById(id, new HisExpMestMedicineLView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE_2 GetLView2ById(long id, HisExpMestMedicineLView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetLView2ById(id, filter.Query());
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
