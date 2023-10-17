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
        internal List<V_HIS_EXP_MEST_5> GetView5(HisExpMestView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView5(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_5 GetView5ById(long id)
        {
            try
            {
                return GetView5ById(id, new HisExpMestView5FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_5 GetView5ById(long id, HisExpMestView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView5ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_5 GetView5ByCode(string code)
        {
            try
            {
                return GetView5ByCode(code, new HisExpMestView5FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_5 GetView5ByCode(string code, HisExpMestView5FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView5ByCode(code, filter.Query());
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
