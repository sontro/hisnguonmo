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
        internal List<L_HIS_EXP_MEST_MATERIAL> GetLView(HisExpMestMaterialLViewFilterQuery filter)
        {
            try
            {
                List<L_HIS_EXP_MEST_MATERIAL> result = DAOWorker.HisExpMestMaterialDAO.GetLView(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
        internal L_HIS_EXP_MEST_MATERIAL GetLViewById(long id)
        {
            try
            {
                return GetLViewById(id, new HisExpMestMaterialLViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal L_HIS_EXP_MEST_MATERIAL GetLViewById(long id, HisExpMestMaterialLViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMaterialDAO.GetLViewById(id, filter.Query());
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
