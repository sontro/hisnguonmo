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
        internal List<V_HIS_EXP_MEST_2> GetView2(HisExpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView2(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_2 GetView2ById(long id)
        {
            try
            {
                return GetView2ById(id, new HisExpMestView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_2 GetView2ById(long id, HisExpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView2ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_2 GetView2ByCode(string code)
        {
            try
            {
                return GetView2ByCode(code, new HisExpMestView2FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_2 GetView2ByCode(string code, HisExpMestView2FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestDAO.GetView2ByCode(code, filter.Query());
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
