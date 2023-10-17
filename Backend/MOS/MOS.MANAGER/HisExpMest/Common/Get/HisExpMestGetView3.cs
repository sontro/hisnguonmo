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
        internal List<V_HIS_EXP_MEST_3> GetView3(HisExpMestView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView3(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_3 GetView3ById(long id)
        {
            try
            {
                return GetView3ById(id, new HisExpMestView3FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_3 GetView3ById(long id, HisExpMestView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView3ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_3 GetView3ByCode(string code)
        {
            try
            {
                return GetView3ByCode(code, new HisExpMestView3FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_3 GetView3ByCode(string code, HisExpMestView3FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView3ByCode(code, filter.Query());
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
