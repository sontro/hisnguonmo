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
        internal List<L_HIS_EXP_MEST_MEDICINE> GetLView(HisExpMestMedicineLViewFilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MEDICINE> result = DAOWorker.HisExpMestMedicineDAO.GetLView(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisExpMestMedicineLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MEDICINE GetLViewById(long id, HisExpMestMedicineLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMedicineDAO.GetLViewById(id, filter.Query());
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
