using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisMaterialBean;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMaterial
{
    partial class HisExpMestMaterialGet : GetBase
    {
        internal List<L_HIS_EXP_MEST_MATERIAL_2> GetLView2(HisExpMestMaterialLView2FilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MATERIAL_2> result = DAOWorker.HisExpMestMaterialDAO.GetLView2(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
        internal L_HIS_EXP_MEST_MATERIAL_2 GetLView2ById(long id)
        {
            try
            {
                return GetLView2ById(id, new HisExpMestMaterialLView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MATERIAL_2 GetLView2ById(long id, HisExpMestMaterialLView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMaterialDAO.GetLView2ById(id, filter.Query());
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
