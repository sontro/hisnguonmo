using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    partial class HisExpMestGet : GetBase
    {
        internal List<V_HIS_EXP_MEST_4> GetView4(HisExpMestView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView4(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_4 GetView4ById(long id)
        {
            try
            {
                return GetView4ById(id, new HisExpMestView4FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_4 GetView4ById(long id, HisExpMestView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView4ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_4 GetView4ByCode(string code)
        {
            try
            {
                return GetView4ByCode(code, new HisExpMestView4FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_4 GetView4ByCode(string code, HisExpMestView4FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView4ByCode(code, filter.Query());
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
