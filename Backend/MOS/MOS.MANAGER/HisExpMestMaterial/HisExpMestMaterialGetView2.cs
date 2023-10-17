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
        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2(HisExpMestMaterialView2FilterQuery filter)
        {
            try
            {
                List<V_HIS_EXP_MEST_MATERIAL_2> result = DAOWorker.HisExpMestMaterialDAO.GetView2(filter.Query(), param);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2BySereServParentIds(List<long> sereServParentIds)
        {
            if (sereServParentIds != null)
            {
                HisExpMestMaterialView2FilterQuery filter = new HisExpMestMaterialView2FilterQuery();
                filter.SERE_SERV_PARENT_IDs = sereServParentIds;
                return this.GetView2(filter);
            }
            return null;
        }

        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2BySereServParentId(long sereServParentId)
        {
            HisExpMestMaterialView2FilterQuery filter = new HisExpMestMaterialView2FilterQuery();
            filter.SERE_SERV_PARENT_ID = sereServParentId;
            return this.GetView2(filter);
        }

        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2ByIds(List<long> ids)
        {
            if (ids != null)
            {
                HisExpMestMaterialView2FilterQuery filter = new HisExpMestMaterialView2FilterQuery();
                filter.IDs = ids;
                return this.GetView2(filter);
            }
            return null;
        }

        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2ByExpMestId(long expMestId)
        {
            HisExpMestMaterialView2FilterQuery filter = new HisExpMestMaterialView2FilterQuery();
            filter.EXP_MEST_ID = expMestId;
            return this.GetView2(filter);
        }

        internal V_HIS_EXP_MEST_MATERIAL_2 GetView2ById(long id)
        {
            try
            {
                return GetView2ById(id, new HisExpMestMaterialView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_MATERIAL_2 GetView2ById(long id, HisExpMestMaterialView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestMaterialDAO.GetView2ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_EXP_MEST_MATERIAL_2> GetView2ByExpMestIds(List<long> expMestIds)
        {
            if (expMestIds != null)
            {
                HisExpMestMaterialView2FilterQuery filter = new HisExpMestMaterialView2FilterQuery();
                filter.EXP_MEST_IDs = expMestIds;
                return this.GetView2(filter);
            }
            return null;
        }
    }
}
